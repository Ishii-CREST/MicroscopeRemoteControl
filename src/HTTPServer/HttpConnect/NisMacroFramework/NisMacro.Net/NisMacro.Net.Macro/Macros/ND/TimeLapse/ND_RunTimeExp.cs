using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    public class ND_RunTimeExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_RunTimeExp()
            : base()
        {
            this.MacroName = "ND_RunTimeExp";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
