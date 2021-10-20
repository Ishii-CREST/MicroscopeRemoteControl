using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ConfocalA1
{
    /// <summary>
    /// 刺激設定をApplyする
    /// </summary>
    public class A1ApplyStimulationSettings : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public A1ApplyStimulationSettings() : base()
        {
            this.MacroName = "A1ApplyStimulationSettings";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
