using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Z
{
    /// <summary>
    /// Moves the primary (main) Z drive to the specified position (absolute movement) or by the specified distance (relative movement).
    /// </summary>
    public class StgMoveMainZ : NisMacroBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public StgMoveMainZ():base()
        {
            this.MacroName = "StgMoveMainZ";
        }

        /// <summary>
        /// Position/distance in micrometers.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam,1)]
        public double StgZ;

        /// <summary>
        /// Movement type (absolute/relative).
        /// 0,MOVE_ABSOLUTE:Absolute movement
        /// 1,MOVE_RELATIVE:Relative movement
        /// </summary>
        [NisMacroAttribute(eParamType.InParam,2)]
        public int relative;

        [NisMacroAttribute(eParamType.Return_Ignore,0)]
        public int Result;
    }
}
