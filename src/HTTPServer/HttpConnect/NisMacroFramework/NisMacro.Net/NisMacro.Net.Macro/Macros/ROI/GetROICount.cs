using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    public class GetROICount:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetROICount() : base()
        {
            this.MacroName = "GetROICount";
        }

        /// <summary>
        /// デフォルトパラメータ
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();
        }

        /// <summary>
        /// 結果
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
