using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    /// <summary>
    /// This function appends a line to the current timelapse experiment table.
    /// </summary>
    public class ND_AppendTimePhaseEx:NisMacroBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public ND_AppendTimePhaseEx(): base()
        {
            this.MacroName = "ND_AppendTimePhaseEx";
        }

        /// <summary>
        /// The interval value in [ms]
        /// 0.0 = No Delay
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public double ND_TimeInterval;

        /// <summary>
        /// Number of loops to be performed within the time phase.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public int ND_TimeCount;

        /// <summary>
        /// A macro command to be performed before the loop.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, true)]
        public string ND_TimeCommand;

        /// <summary>
        /// Name of the phase
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 4, true)]
        public string ND_PhaseName;
    
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
