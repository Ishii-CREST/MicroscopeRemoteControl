using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace NisMacro.Net.Execute
{
    /// <summary>
    /// マクロ実行キュークラス（シングルトン）
    /// </summary>
    public class MacroQueue
    {
        /// <summary>
        /// Nisマクロ実行クラス
        /// </summary>
        public NisMacroExecuter nisMacroExecuter { get; set; } = new NisMacroExecuter();
        /// <summary>
        /// インスタンス
        /// </summary>
        private static MacroQueue myInstance = new MacroQueue();
        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static MacroQueue GetInstance { get { return myInstance; } }
        /// <summary>
        /// ポーリング継続フラグ
        /// </summary>
        public bool _pollingFlag;
        private MacroQueue() { }

        /// <summary>
        /// キューリスト
        /// </summary>
        private List<MacroManageUnit> _macroList = new List<MacroManageUnit>();

        /// <summary>
        /// 管理用クラス
        /// </summary>
        public class MacroManageUnit
        {
            /// <summary>
            /// 実行対象マクロ
            /// </summary>
            public NisMacro.Net.Macro.NisInterface.IMacro exeMacro { get; set; }
            /// <summary>
            /// 同期用イベント
            /// </summary>
            public AutoResetEvent endEvent = new AutoResetEvent(false);
        }

        /// <summary>
        /// キューにマクロを追加し、非同期で実行します。
        /// ※マクロの実行自体は同期です。
        /// </summary>
        /// <param name="macro"></param>
        public void AddMacro(NisMacro.Net.Macro.NisInterface.IMacro macro)
        {
            try
            {

                if (!_pollingFlag)
                {
                    Console.WriteLine("Macro Polling thread is not running. Macro execute is canceled.");
                }
                else
                {
                    _macroList.Add(new MacroManageUnit() { exeMacro = macro });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// キューにマクロを追加し、実行されるまで待機します。
        /// </summary>
        /// <param name="macro"></param>
        public void SyncAddMacro(NisMacro.Net.Macro.NisInterface.IMacro macro)
        {
            try
            {

                if (!_pollingFlag)
                {
                    Console.WriteLine("Macro Polling thread is not running. Macro execute is canceled.");
                }
                else
                {
                    MacroManageUnit addUnit = new MacroManageUnit() { exeMacro = macro };
                    _macroList.Add(addUnit);
                    addUnit.endEvent.WaitOne();     // 終了を待機する。
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// ポーリングを開始する。
        /// </summary>
        public void StartPoling()
        {
            try
            {
                _pollingFlag = true;
                Task.Run(() => { Polling(); });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// ポーリングを中止する。
        /// </summary>
        public void StopPolling()
        {
            _pollingFlag = false;
        }
        /// <summary>
        /// スレッドが生きているか取得する
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return _pollingFlag;
            }
        }
        /// <summary>
        /// マクロキューの処理
        /// </summary>
        private void Polling()
        {
            MacroManageUnit data = null;
            while (_pollingFlag)
            {
                data = null;
                if (_macroList.Count > 0)
                {
                    data = _macroList[0];
                    _macroList.RemoveAt(0);     // リストから取り除きます。(Deque)

                    nisMacroExecuter.ExecuteSync(data.exeMacro);     // マクロを実行します。

                    data.endEvent.Set();        // 同期イベントをセットします。
                }
                System.Threading.Thread.Sleep(10);
            }
            if (data != null)
            {
                data.endEvent.Set();
            }
        }
    }
}
