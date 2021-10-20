using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Stimulation
{
    /// <summary>
    /// The function appends a time phase to the current sequential stimulation experiment.
    /// </summary>
    public class ND_StimulationAppendPhase:NisMacroBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ND_StimulationAppendPhase() : base()
        {
            this.MacroName = "ND_StimulationAppendPhase";
        }

        /// <summary>
        /// Type of the phase to be appended.
        /// -1:Waiting - it only works when the ND_TimeInterval parameter is set to "-1".
        /// 0:Acquisition
        /// 1:Stimulation
        /// 2:Bleaching
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int PhaseType;
        /// <summary>
        /// The interval value in [ms]
        /// 0.0:No Delay

        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public double ND_TimeInterval;
        /// <summary>
        /// Phase duration [ms].
        /// -1:Continuous
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public double ND_TimeDuration;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
