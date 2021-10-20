using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Nosepiece
{
    /// <summary>
    /// 対物レンズ台有無取得
    /// </summary>
    public class Stg_IsNosepiecePresent : NisMacroBase
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public Stg_IsNosepiecePresent() : base()
        {
            this.MacroName = "Stg_IsNosepiecePresent";
        }


        /// <summary>
        /// 1 = Nosepiece is present.
        /// 0 = Nosepiexe is not Present.
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
