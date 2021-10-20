using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Util
{
    /// <summary>
    /// Nis Elements管理クラス
    /// </summary>
    /// <remarks>singleton Patern</remarks>
    public class NisArMonitor
    {
        private Process nisArProcess;
        public IntPtr nisMainWindowHandle { get; private set; }

        /// <summary>
        /// チェック間隔(msec)
        /// </summary>
        private const int CHECK_WAIT = 1000;

        /// <summary>
        /// Nis Elementsの本体プロセス名
        /// </summary>
        private string NIS_ELEMENTS_PRCESS_NAME = "nis_ar";

        /// <summary>
        /// Nis Elements終了時イベント
        /// </summary>
        public EventHandler EventNisElementsFinished = null;

        /// <summary>
        /// インスタンス
        /// </summary>
        private static NisArMonitor _instance;//= new NisArMonitor();

        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static NisArMonitor Instance
        {
            get
            {
                if(_instance == null) _instance = new NisArMonitor();
                return _instance;
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private NisArMonitor()
        {
            //  初期化処理
            this.Initialize();
        }

        /// <summary>
        /// このプロセスがNisElementsから起動されたかどうか
        /// </summary>
        /// <remarks>単体起動は想定していないそうだが、今後の拡張を加味</remarks>
        public bool OnNisArProcess
        {
            get;
            private set;
        }

        /// <summary>
        /// Nis Elemetnsプロセス(本システムの親プロセス)が生きているかを取得する
        /// </summary>
        /// <remarks>true == Nis Elementsの親画面は未だ健在 / false == Nis Elementsは終了した</remarks>
        public bool IsNisElementsAlive
        {
            get
            {
                //  返却用フラグ
                bool isAlive = false;
                nisArProcess = this.GetNisArProcess;

                if ( null != nisArProcess )
                {
                    //  Nis Elementsが終了した場合、MainWindowHandleが空になるのを利用して終了判定をする
                    if ( nisArProcess.MainWindowHandle != IntPtr.Zero )
                    {
                        isAlive = true;
                    }
                }

                return isAlive;
            }
        }

        /// <summary>
        /// Nis Elementsプロセスを取得する
        /// </summary>
        /// <remarks>NISが起動していない場合はnullを返す</remarks>
        public Process GetNisArProcess
        {
            get
            {
                Process[] nisArProcesses = Process.GetProcessesByName( NIS_ELEMENTS_PRCESS_NAME );

                //  NISは1プロセスのみ起動できるため、2以上はありえない
                if( 0 < nisArProcesses.Length )
                {
                    foreach(var p in nisArProcesses)
                    {
                        if(p.MainWindowTitle != "")
                        {
                            return p;
                        }
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void Initialize()
        {
            //  NISプロセスを取得
            nisArProcess = this.GetNisArProcess;
            if ( nisArProcess != null )
            {
                nisMainWindowHandle = nisArProcess.MainWindowHandle;
            }
            //  初期化時にNisElementsがいればNisから起動したとみなす
            this.OnNisArProcess = this.IsNisElementsAlive;
        }

        /// <summary>
        /// Nis監視スレッドの起動
        /// </summary>
        public void StartMonitorThread()
        {
            //  監視用スレッド(BackgroundWorker)作成
            BackgroundWorker bgwMonitoringNisAr = new BackgroundWorker();
            bgwMonitoringNisAr.DoWork += this.bgwMonitoringNisAr_DoWork;
            bgwMonitoringNisAr.RunWorkerCompleted += this.bgwMonitoringNisAr_RunWorkerCompleted;
            bgwMonitoringNisAr.RunWorkerAsync();
        }

        /// <summary>
        /// Nis Elements監視スレッドの実行処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMonitoringNisAr_DoWork( object sender, DoWorkEventArgs e )
        {
            //  メインハンドルが存在するかをチェックし続ける
            while( true )
            {
                //  Nis Elementsがいるか判定する
                if( false == this.IsNisElementsAlive )
                {
                    //  Nisが終了した場合、Threadを抜ける
                    return;
                }
                else
                {
                    //  次のチェック間隔まで待機
                    System.Threading.Thread.Sleep( CHECK_WAIT );
                }
            }
        }

        /// <summary>
        /// Nis Elements監視スレッドの終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMonitoringNisAr_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            //  NisElementsの終了イベントを実行する
            if( null != this.EventNisElementsFinished )
            {
                this.EventNisElementsFinished( this, new EventArgs() );
            }
        }
    }

}
