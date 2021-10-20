using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Stimulation
{
    /// <summary>
    ///The function removes all phases settings of the 
    ///current sequential stimulation experiment.
    /// </summary>
    public class ND_StimulationResetPhases : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_StimulationResetPhases()
            : base()
        {
            this.MacroName = "ND_StimulationResetPhases";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
