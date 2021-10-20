using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.MultiPoint
{
    public class ND_MP_GetCount : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_MP_GetCount()
            : base()
        {
            this.MacroName = "ND_MP_GetCount";
        }
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
