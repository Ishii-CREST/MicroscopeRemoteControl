using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Twain
{
    /// <summary>
    /// 撮像の開始要求
    /// </summary>
    public class Live : NisMacroBase
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public Live() : base()
        {
            this.MacroName = "Live";
        }


        /// <summary>
        /// 1 = Nosepiece is present.
        /// 0 = Nosepiexe is not Present.
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
