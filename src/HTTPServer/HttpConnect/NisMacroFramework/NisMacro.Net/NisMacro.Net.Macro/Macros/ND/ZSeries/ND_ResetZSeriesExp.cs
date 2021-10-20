using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.ZSeries
{
    /// <summary>
    /// The last used settings of the experiment are stored 
    /// in memory. This function removes the settings of 
    /// the current Z-series experiment.
    /// </summary>
    public class ND_ResetZSeriesExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_ResetZSeriesExp()
            : base()
        {
            this.MacroName = "ND_ResetZSeriesExp";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
