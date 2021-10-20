using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraSet_StimulationTimen
    /// </summary>
    public class CameraSet_StimulationTimen:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraSet_StimulationTimen(eStimID id) : base()
        {
            this.MacroName = string.Format("CameraSet_StimulationTime{0}", (int)id);
        }

        /// <summary>
        /// 刺激時間
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
