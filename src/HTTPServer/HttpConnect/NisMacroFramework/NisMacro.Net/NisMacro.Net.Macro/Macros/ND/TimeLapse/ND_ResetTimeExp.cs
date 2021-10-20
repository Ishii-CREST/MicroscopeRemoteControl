using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    /// <summary>
    /// The last used settings of the experiment are stored in memory. 
    /// This function removes the settings of the current time experiment. 
    /// The experiment definition table becomes blank.
    /// </summary>
    public class ND_ResetTimeExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_ResetTimeExp()
            : base()
        {
            this.MacroName = "ND_ResetTimeExp";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
