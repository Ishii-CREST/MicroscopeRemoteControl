using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    public class GetROIIdFromIndex:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetROIIdFromIndex() : base()
        {
            this.MacroName = "GetROIIdFromIndex";
        }

        /// <summary>
        /// デフォルトパラメータ
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();
        }

        /// <summary>
        /// ROI index among visible ROIs. The first index is 0, the last can be obtained using function GetROICount-1
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public int RoiIndex;

        /// <summary>
        /// 結果
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}

