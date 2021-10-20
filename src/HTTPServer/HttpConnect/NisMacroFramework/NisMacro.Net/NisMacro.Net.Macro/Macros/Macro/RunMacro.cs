using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Macro
{
    /// <summary>
    /// The RunMacro function executes MacroFileName macro.
    /// </summary>
    public class RunMacro:NisMacroBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        public RunMacro():base()
        {
            this.MacroName = "RunMacro";
        }

        /// <summary>
        /// Name of the macro file including file path. E.g. “C:\macros\mymacro.mac” 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam,1)]
        public string MacroFileName;
        /// <summary>
        /// Macro Result
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore,0)]
        public int Result;
    }
}
