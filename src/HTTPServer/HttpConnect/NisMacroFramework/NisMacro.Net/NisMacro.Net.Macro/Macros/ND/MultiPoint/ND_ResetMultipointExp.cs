using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.MultiPoint
{
    /// <summary>
    /// The last used settings of the experiment are stored in memory. 
    /// This function removes the settings of the current multipoint experiment.
    /// The experiment definition table becomes blank.
    /// </summary>
    public class ND_ResetMultipointExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_ResetMultipointExp()
            : base()
        {
            this.MacroName = "ND_ResetMultipointExp";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
