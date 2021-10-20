using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.InternalData.NIS_Elements
{
    /// <summary>
    /// 内部データ保持クラス
    /// </summary>
    public class NisInternalData
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static NisInternalData myInstance = new NisInternalData();
        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static NisInternalData Instance { get { return myInstance; } }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private NisInternalData()
        {
            EnableNisMacroFinishEvent();
            EnableNisMacroStartEvent();
        }

        /// <summary>
        /// ND Experiment中か否か
        /// </summary>
        public bool IsRunningNDExpriment { get; set; }
        /// <summary>
        /// ND Stimulation中か否か
        /// </summary>
        public bool IsRunningNDStimulation { get; set; }
        /// <summary>
        /// Capture中か否か
        /// </summary>
        public bool IsCapturering { get; set; }
        /// <summary>
        /// 最後に実行したND Experimentの実行情報
        /// </summary>
        public NDExperimentInfo LastNDExperimentInfo { get; set; } = new NDExperimentInfo();
        /// <summary>
        /// 画像公開用フォルダに配置するWeb.Configのパス
        /// </summary>
        public string WebConfigPath
        {
            get
            {
                return Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName,
                            CommonDefine.DIR_ORG_FILE, CommonDefine.WEB_CONFIG);
            }
        }
        /// <summary>
        /// NISマクロ実行開始イベントを紐付ける
        /// </summary>
        public void EnableNisMacroFinishEvent()
        {
            NisMacro.Net.Macro.CommandManager.GetInstance.NisMacroFinishEvent += GetInstance_NisMacroFinishEvent;
        }
        /// <summary>
        /// NISマクロ実行終了イベントを紐付ける
        /// </summary>
        public void EnableNisMacroStartEvent()
        {
            NisMacro.Net.Macro.CommandManager.GetInstance.NisMacroStartEvent += GetInstance_NisMacroStartEvent; ;
        }

        /// <summary>
        /// NISマクロ実行開始イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetInstance_NisMacroStartEvent(object sender, NisMacro.Net.Macro.MacroEventArgs e)
        {
            if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.ND.Experiment.ND_RunExperiment().MacroName)
            {
                IsRunningNDExpriment = true;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Start ND_RunExperiment : ND Experiment is running."));
            }
            else if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.ND.Stimulation.ND_RunSequentialStimulationExp().MacroName)
            {
                IsRunningNDStimulation = true;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Start ND_RunSequentialStimulationExp : ND ND_SequentialStimulationExp is running."));
            }
            else if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.Acquire.Capture.Capture().MacroName)
            {
                IsCapturering = true;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Start Capture : ND Capture is running."));
            }
        }
        /// <summary>
        /// NISマクロ実行完了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetInstance_NisMacroFinishEvent(object sender, NisMacro.Net.Macro.MacroEventArgs e)
        {
            if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.ND.Experiment.ND_RunExperiment().MacroName)
            {
                IsRunningNDExpriment = false;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Finish ND_RunExperiment : ND Experiment is finished."));
            }
            else if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.ND.Stimulation.ND_RunSequentialStimulationExp().MacroName)
            {
                IsRunningNDStimulation = false;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Finish ND_RunSequentialStimulationExp : ND ND_SequentialStimulationExp is finished."));
            }
            else if (e.Imacro.GetMacroName() == new NisMacro.Net.Macro.Macros.Acquire.Capture.Capture().MacroName)
            {
                IsCapturering = false;
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("Receive Finish Capture : ND Capture is finished."));
            }
        }
    }
}
