using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    /// <summary>
    /// ROIの描画を削除する
    /// </summary>
    public class ClearMeasROI : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClearMeasROI() : base()
        {
            this.MacroName = "ClearMeasROI";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
