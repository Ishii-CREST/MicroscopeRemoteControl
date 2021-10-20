using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NisMacro.Net.Macro.NisInterface;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// シングルトンのマクロコマンド管理クラス
    /// </summary>
    public class CommandManager
    {
        #region シングルトン宣言

        private static CommandManager _manager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CommandManager()
        {
        }

        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static CommandManager GetInstance
        {
            get
            {
                if( null == _manager )
                {
                    _manager = new CommandManager();
                }

                return _manager;
            }
        }

        #endregion

        /// <summary>
        /// コマンドを実行中かどうか
        /// </summary>
        private bool m_IsExecuting;

        /// <summary>
        /// Nisコマンドの完了通知デリゲート
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベント</param>
        public delegate void NisMacroEventHadler(object sender, MacroEventArgs e);
        /// <summary>
        /// Niコマンドの完了通知イベント
        /// </summary>
        public event NisMacroEventHadler NisMacroFinishEvent;

        public event NisMacroEventHadler NisMacroStartEvent;


        public delegate int DelegateProc(int aiType);

        public event DelegateProc EventTriggerevent;

        #region 変数
        /// <summary>
        /// Nis上に変数を定義する
        /// </summary>
        /// <param name="iVariable">IVariableインターフェース継承クラス</param>
        /// <param name="value">値</param>
        public void SetVariable( IVariable iVariable, object value )
        {
            try
            {
                //  既に実行中の場合は例外発生
                if( true == this.m_IsExecuting )
                {
                    throw new Exception( "コマンドは既に実行中です。" );
                }

                this.m_IsExecuting = true;

                //  Nis上にインスタンスする
                //  データリスト初期化
                StoredData.GetInstance.ClearDataList();
                StoredData.GetInstance.AddData( iVariable.GetType(), value );

                /*
                 *  文字列作成を行い同期で実行
                 */
                NisMacro.Net.Interprocess.Command.GetInstance().Execute( iVariable.CreateSetCmd() );

                this.m_IsExecuting = false; //  実行中フラグ制御
            }
            catch( Exception ex )
            {
                Console.Write( ex );
            }
        }

        /// <summary>
        /// Nis上の変数を取得する
        /// </summary>
        /// <param name="iVariable">IVariableインターフェース継承クラス</param>
        /// <returns>値</returns>
        public object GetVariable( IVariable iVariable )
        {
            try
            {
                //  既に実行中の場合は例外発生
                if( true == this.m_IsExecuting )
                {
                    throw new Exception( "コマンドは既に実行中です。" );
                }

                this.m_IsExecuting = true; //  実行中フラグ制御

                //  データリスト初期化
                StoredData.GetInstance.ClearDataList();

                //  データ追加(取得用に空のデータ構造を作成しておく)
                StoredData.GetInstance.AddData( iVariable.GetVariableType(), null );

                /*
                 *  文字列作成を行い同期で実行
                 */
                NisMacro.Net.Interprocess.Command.GetInstance().Execute( iVariable.CreateGetCmd() );

                int dataIndex = 0;  //  変数のときのIndexは0固定とする

                this.m_IsExecuting = false; //  実行中フラグ制御

                //  取得できた値を返す
                return StoredData.GetInstance.GetData( dataIndex, iVariable.GetVariableType() );
            }
            catch( Exception ex )
            {
                Console.Write( ex );
                return null;
            }
        }

        #endregion

        #region マクロ
        /// <summary>
        /// NIS-ElementsからCallされるメソッドイベント
        /// </summary>
        public void SendEventTrigger(int aiType)
        {
            try
            {
                EventTriggerevent(aiType);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                throw;
            }
        }
        /// <summary>
        /// マクロ実行
        /// </summary>
        /// <param name="imacro">マクロインターフェース継承済みクラス</param>
        public IMacro SendMacro( IMacro imacro )
        {
            try
            {
                //  既に実行中の場合は例外発生
                if( true == this.m_IsExecuting )
                {
                    throw new Exception( "コマンドは既に実行中です。" );
                }

                this.m_IsExecuting = true; //  実行中フラグ制御

                if (null != NisMacroStartEvent)
                {
                    NisMacroStartEvent(this, new MacroEventArgs(imacro));
                }

                this.ExecuteCore( ref imacro );

                this.m_IsExecuting = false; //  実行中フラグ制御

                /*
                 *  イベント発生
                 */
                if( null != NisMacroFinishEvent )
                {
                    NisMacroFinishEvent( this, new MacroEventArgs( imacro ) );
                }
                
                return imacro;
            }
            catch( Exception ex )
            {
                Console.Write( ex );
                throw;
            }
        }

        private void ExecuteCore( ref IMacro imacro )
        {
            //  データリスト初期化
            StoredData.GetInstance.ClearDataList();

            //  パラメータ部分のメンバーを取得して、Index順にソートする
            List<ExMemberInfo> members = imacro.GetNisParamMemberInfo( eParamType.InParam, eParamType.OutParam );
            members.Sort( ( a, b ) => a.Index - b.Index );

            /*
             *  リザルトをセット
             */
            List<ExMemberInfo> resMember = imacro.GetNisParamMemberInfo( eParamType.Return );
            if( 0 != resMember.Count )
            {
                StoredData.GetInstance.AddData( resMember[0].ValueType, resMember[0].Value );
            }
            else
            {
                //  ない場合もIndexをそろえるためint'0'の値を追加しておく
                StoredData.GetInstance.AddData( typeof( System.Int32 ), 0 );
            }

            /*
             *  パラメータをセット
             */
            foreach( ExMemberInfo paramMember in members )
            {
                StoredData.GetInstance.AddData( paramMember.ValueType, paramMember.Value );
            }

            /*
             *  文字列作成を行い同期で実行
             */
            if (NisMacro.Net.Interprocess.Command.GetInstance().Execute(imacro.CreateCmd()) != 0) throw new Exception("Command execution has failed.");


            //  結果(Result、OutParam)を取得
            List<ExMemberInfo> newMembers = imacro.GetNisParamMemberInfo( eParamType.InParam, eParamType.OutParam, eParamType.Return, eParamType.Return_Ignore );
            newMembers.Sort( ( a, b ) => a.Index - b.Index );
            for( int index = 0; index < StoredData.GetInstance.GetData().Count(); index++ )
            {
                newMembers[index].Value = StoredData.GetInstance.GetData( index, newMembers[index].ValueType );
            }

            /*
             *  Nisから取得したパラメータをクラスに戻す
             */
            imacro.SetNisParamMemberInfo( newMembers );
        }

        /// <summary>
        /// マクロ実行(同期)
        /// </summary>
        /// <param name="iMacros">マクロインターフェース継承済みクラス(先頭より順次実行)</param>
        /// <returns>実行結果(引数順)</returns>
        public List<IMacro> SendMacro( params IMacro[] iMacros )
        {
            try
            {
                //  既に実行中の場合は例外発生
                if( true == this.m_IsExecuting )
                {
                    throw new Exception( "コマンドは既に実行中です。" );
                }

                this.m_IsExecuting = true; //  実行中フラグ制御

                List<IMacro> resList = new List<IMacro>();

                //  引数のマクロを順次実行して返却用のリストに格納
                foreach( IMacro iMacro in iMacros )
                {
                    IMacro iResMacro = this.SendMacro( iMacro );
                    resList.Add( iResMacro );
                }

                this.m_IsExecuting = false; //  実行中フラグ制御

                return resList;
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex );
                throw;
            }
        }


        public class ASyncSetting
        {            
            /// <summary>
            /// 実行マクロ
            /// </summary>
            public IMacro Imacro
            {
                get;
                set;
            }
            /// <summary>
            /// タイムアウト時間(msec)
            /// </summary>
            public int TimeOutMSec
            {
                get;
                private set;
            }

            /// <summary>
            /// タイムアウトしたかどうか
            /// </summary>
            public bool IsTimeout
            {
                get;
                set;
            }

            /// <summary>
            /// コールバック関数
            /// </summary>
            public NisMacroEventHadler CallBack
            {
                get;
                private set;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="imacro">マクロ</param>
            /// <param name="callBack">コールバック関数</param>
            /// <param name="timeOutMSec">タイムアウト時間</param>
            public ASyncSetting( IMacro imacro, NisMacroEventHadler callBack, int timeOutMSec )
            {
                this.Imacro = imacro;
                this.CallBack = callBack;
                this.TimeOutMSec = timeOutMSec;
                this.IsTimeout = false;
            }
        }
        
        /// <summary>
        /// マクロ実行(非同期)
        /// </summary>
        /// <param name="imacro">マクロインターフェース継承済みクラス</param>
        /// <param name="callBack">コールバック先</param>
        /// <param name="timeOutMSec">タイムアウト時間(msec)</param>
        public void SendMacroASync( IMacro imacro, NisMacroEventHadler callBack = null, int timeOutMSec = 3000 )
        {
            try
            {
                //  既に実行中の場合は例外発生
                if( true == this.m_IsExecuting )
                {
                    throw new Exception( "コマンドは既に実行中です。" );
                }

                this.m_IsExecuting = true; //  実行中フラグ制御

                BackgroundWorker bgwMacroASync = new BackgroundWorker();    //  マクロ実行用スレッド
                BackgroundWorker bgwTimeout = new BackgroundWorker();       //  タイムアウト管理用スレッド
                
                //  実処理イベント接続
                bgwMacroASync.DoWork += bgwMacroASync_DoWork;
                bgwTimeout.DoWork += bgwTimeout_DoWork;

                //  完了イベントはマクロ実処理、タイムアウト両方とも同じ
                bgwMacroASync.RunWorkerCompleted += bgwMacroASync_RunWorkerCompleted;
                bgwTimeout.RunWorkerCompleted += bgwMacroASync_RunWorkerCompleted;

                //  設定クラスをそれぞれインスタンス
                ASyncSetting asyncSettingExe = new ASyncSetting( imacro, callBack, timeOutMSec );
                ASyncSetting asyncSettingCancel = new ASyncSetting( imacro, callBack, timeOutMSec );

                //  マクロ、タイムアウトの2つのスレッド非同期で実行
                bgwMacroASync.RunWorkerAsync( asyncSettingExe );
                bgwTimeout.RunWorkerAsync( asyncSettingCancel );
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex );
                throw;
            }
        }

        
        /// <summary>
        /// マクロ実行実処理部分(非同期)
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベント(Argumentに設定情報を付加)</param>
        private void bgwMacroASync_DoWork( object sender, DoWorkEventArgs e )
        {
            //  引数から設定値を取得
            ASyncSetting asyncSetting = (ASyncSetting)e.Argument;

            //  Refを行うため、変数に退避
            IMacro tempImacro = asyncSetting.Imacro;

            //  マクロ実行実処理
            this.ExecuteCore( ref tempImacro );

            //  変数から設定クラスに戻す
            asyncSetting.Imacro = tempImacro;

            //  結果に戻す
            e.Result = asyncSetting;
        }

        /// <summary>
        /// タイムアウト実処理
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベント(Argumentに設定情報を付加)</param>
        private void bgwTimeout_DoWork( object sender, DoWorkEventArgs e )
        {
            //  引数から設定値を取得
            ASyncSetting asyncSetting = (ASyncSetting)e.Argument;

            //  タイムアウト時間分待機
            Thread.Sleep( asyncSetting.TimeOutMSec );

            //  タイムアウトなし
            asyncSetting.IsTimeout = true;
        }

        /// <summary>
        /// 実行完了
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベント</param>
        void bgwMacroASync_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            try
            {
                //  マクロ実処理、タイムアウト実処理のどちらか先に来たスレッドだけ以下の処理を行う
                if( this.m_IsExecuting )
                {
                    this.m_IsExecuting = false; //  実行中フラグ制御

                    /*
                     *  イベント発生
                     */
                    this.m_IsExecuting = false; //  実行中フラグ制御
                    if( null != NisMacroFinishEvent )
                    {
                        NisMacroFinishEvent( this, new MacroEventArgs( ( (ASyncSetting)e.Result ).Imacro ) );
                    }
                }
            }
            catch( Exception ex )
            {
                Console.WriteLine( ex );
                throw;
            }
        }
        #endregion
    }
}
