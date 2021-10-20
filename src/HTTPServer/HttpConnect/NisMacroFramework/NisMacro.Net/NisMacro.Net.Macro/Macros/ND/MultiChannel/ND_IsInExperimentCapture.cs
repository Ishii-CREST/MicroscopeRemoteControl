using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.MultiChannel
{
    public class ND_IsInExperimentCapture : NisMacroBase
    {
        public ND_IsInExperimentCapture() : base()
        {
            this.MacroName = "ND_IsInExperimentCapture";
        }
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
