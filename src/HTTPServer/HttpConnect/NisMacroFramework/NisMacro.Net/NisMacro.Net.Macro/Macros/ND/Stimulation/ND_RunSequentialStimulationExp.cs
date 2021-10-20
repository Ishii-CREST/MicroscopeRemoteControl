using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Stimulation
{
    public class ND_RunSequentialStimulationExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_RunSequentialStimulationExp() : base()
        {
            this.MacroName = "ND_RunSequentialStimulationExp";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
