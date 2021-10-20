using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NisMacro.Net.Execute
{
    /// <summary>
    /// NISコマンドを実行するためのダミーウインドウクラス
    /// </summary>
    public class NisMacroExecuter : NativeWindow, IDisposable
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
        /// マクロ実行メソッド
        /// </summary>
        /// <param name="macro"></param>
        /// <returns></returns>
        private delegate NisMacro.Net.Macro.NisInterface.IMacro _Execute(NisMacro.Net.Macro.NisInterface.IMacro macro);
        /// <summary>
        /// 実行
        /// </summary>
        private _Execute _executeMethod;
        /// <summary>
        /// マクロ実行単位
        /// </summary>
        public NisCommandExeUnit _macro { get; set; }
        /// <summary>
        /// アプリ定義
        /// </summary>
        private const int WM_APP = 0x8000;
        /// <summary>
        /// マクロ開始
        /// </summary>
        public const int WM_EXE_MACRO = WM_APP + 1;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NisMacroExecuter()
        {
            // ウインドウの作成
            // 表示されないウインドウを作成する。
            CreateParams cp = new CreateParams();
            cp.X = 0;
            cp.Y = 0;
            cp.Height = 0;
            cp.Width = 0;
            cp.Style = 0x00800000;

            // マクロ実行メソッドの取得
            _executeMethod = new _Execute(NisMacro.Net.Macro.CommandManager.GetInstance.SendMacro);

            this.CreateHandle(cp);
        }
        /// <summary>
        /// ウインドウメッセージの処理
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if ( m.Msg == WM_EXE_MACRO)
            {
                if ( _macro != null )
                {
                    _executeMethod(_macro.exeMacro);    // マクロ実行
                    _macro.isCompete = true;            // 終了フラグ
                }
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// マクロの実行メソッド。メインスレッドで実行。
        /// </summary>
        /// <param name="macro"></param>
        /// <returns></returns>
        public NisMacro.Net.Macro.NisInterface.IMacro ExecuteSync(NisMacro.Net.Macro.NisInterface.IMacro macro)
        {
            if ( NisMacro.Net.Util.NisArMonitor.Instance.IsNisElementsAlive )
            {
                //メインスレッドで実行するためにPostMessage
                _macro = new NisCommandExeUnit(macro);
                if ( this.Handle != IntPtr.Zero )
                {
                    PostMessage(this.Handle, NisMacroExecuter.WM_EXE_MACRO, 0, 0);
                    while ( !_macro.isCompete && MacroQueue.GetInstance.IsAlive ) { System.Threading.Thread.Sleep(10); }
                }
            }
            else
            {
                Console.WriteLine("<!>NIS-Elements is not running. NisCommand has not executed.");
            }
            return null;
        }

        /// <summary>
        /// 破棄する
        /// </summary>
        public void Dispose()
        {
            DestroyHandle();
        }
    }
}
