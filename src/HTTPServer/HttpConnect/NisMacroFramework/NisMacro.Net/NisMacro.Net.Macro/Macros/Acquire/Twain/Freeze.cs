using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Twain
{
    /// <summary>
    /// 撮像の停止要求
    /// </summary>
    public class Freeze : NisMacroBase
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public Freeze() : base()
        {
            this.MacroName = "Freeze";
        }


        /// <summary>
        /// 1 = Nosepiece is present.
        /// 0 = Nosepiexe is not Present.
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
