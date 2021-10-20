using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisMacro.Net.Execute
{

    /// <summary>
    /// マクロGUIをメインスレッドで実行するためのクラス
    /// </summary>
    public class NisExecuteDummy : NativeWindow, IDisposable
    {
        /// <summary>
        /// PostMessage
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// インスタンス
        /// </summary>
        private static NisExecuteDummy myInscance;
        /// <summary>
        /// インスタンスを取得
        /// </summary>
        public static NisExecuteDummy Instance
        {
            get
            {
                if ( myInscance == null )
                {
                    // 取得しようとした時に生成。
                    myInscance = new NisExecuteDummy();
                }
                return myInscance;
            }
        }
        /// <summary>
        /// 破棄メッセージ
        /// </summary>
        private const int WM_DESTROY = 0x0002;
        /// <summary>
        /// アプリ定義
        /// </summary>
        private const int WM_APP = 0x8000;
        /// <summary>
        /// マクロ開始
        /// </summary>
        public const int WM_EXE_MACRO = WM_APP + 1;
        /// <summary>
        /// 実際に表示するメイン画面
        /// </summary>
        private Form _MainWindow;

        /// <summary>
        /// Nisが終了しているか否か
        /// </summary>
        public bool _isNisFinished;
        /// <summary>
        /// マクロ実行単位
        /// </summary>
        private NisCommandExeUnit _macro { get; set; }
        /// <summary>
        /// メインウインドウの表示タスク
        /// </summary>
        private Task _mainShowTask { get; set; }
        /// <summary>
        /// 終了処理のタスク
        /// </summary>
        private Task finishTask { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private NisExecuteDummy()
        {
            if ( NisMacro.Net.Util.NisArMonitor.Instance.nisMainWindowHandle != IntPtr.Zero )
            {
                this.AssignHandle(NisMacro.Net.Util.NisArMonitor.Instance.nisMainWindowHandle);         // NISハンドルの設定　メッセージをフックするため。
            }
        }

        /// <summary>
        /// ウインドウメッセージの処理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            // NISの破棄を検知する
            if ( m.Msg == WM_DESTROY )
            {
                _isNisFinished = true;
                finishTask = Task.Run(() => OnNisFinished());   // 終了タスク開始
                finishTask.Wait();      // 待機
                this.ReleaseHandle();   // NISハンドルを解放
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// キーストロークを送信する。
        /// </summary>
        /// <param name="k"></param>
        public void SendKey(Keys k)
        {
            PostMessage(this.Handle, 0x0100, (int)k, 0);
        }

        /// <summary>
        /// 終了時処理
        /// </summary>
        private void EndProc()
        {
            MacroQueue.GetInstance.StopPolling();  // ポーリング停止
        }

        /// <summary>
        /// NIS終了時処理
        /// </summary>
        private void OnNisFinished()
        {
            EndProc();
            if ( _MainWindow != null )
            {
                _MainWindow.BeginInvoke(new MethodInvoker(_MainWindow.Close));
                if ( _mainShowTask != null ) _mainShowTask.Wait();
            }
        }
        /// <summary>
        /// メイン画面終了時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _child_FormClosed(object sender, FormClosedEventArgs e)
        {
            EndProc();
        }

        /// <summary>
        /// マクロ待機開始
        /// </summary>
        public void StartWaitMacro()
        {
            MacroQueue.GetInstance.StartPoling();          // ポーリング開始
        }

        /// <summary>
        /// メイン画面として表示する画面を登録する。
        /// </summary>
        /// <param name="child"></param>
        public void SetMainWindow(Form child)
        {
            if ( _MainWindow != null )
            {
                // 既に設定されている
                throw new Exception("NIS macro applicarion Main Window has already exists.");
            }
            else if ( child == null )
            {
                // null
                throw new NullReferenceException();
            }
            else
            {
                _MainWindow = child;
                _MainWindow.FormClosed += _child_FormClosed;
            }
        }

        /// <summary>
        /// メイン画面となる画面を表示する
        /// </summary>
        public void ShowMainWindow()
        {
            if ( _MainWindow != null )
            {
                // STAで開始する。
                // メッセージループはNISと分けるので、Application.Runで開始。
                _mainShowTask = NisMacro.Net.Util.STATask.Run(() =>
                {
                    Application.Run(_MainWindow);    // メインウインドウ開始
                    RemoveMainWindow();              //終わったら取り除く
                    this.ReleaseHandle();
                });
            }
        }

        /// <summary>
        /// メイン画面となる画面の登録を削除する。
        /// </summary>
        public void RemoveMainWindow()
        {
            _MainWindow = null;
        }


        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            if ( _mainShowTask != null )
            {
                _mainShowTask.Wait();
                _mainShowTask.Dispose();
            }
            if ( finishTask != null )
            {
                finishTask.Wait();
                finishTask.Dispose();
            }
        }
    }
}