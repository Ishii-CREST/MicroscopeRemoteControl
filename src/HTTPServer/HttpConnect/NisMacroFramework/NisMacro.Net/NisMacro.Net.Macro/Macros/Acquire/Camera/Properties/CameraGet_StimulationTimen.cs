using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraGet_StimulationTimen
    /// </summary>
    public class CameraGet_StimulationTimen:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraGet_StimulationTimen(eStimID id) : base()
        {
            this.MacroName = string.Format("CameraGet_StimulationTime{0}", (int)id);
        }

        /// <summary>
        /// 刺激時間
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
