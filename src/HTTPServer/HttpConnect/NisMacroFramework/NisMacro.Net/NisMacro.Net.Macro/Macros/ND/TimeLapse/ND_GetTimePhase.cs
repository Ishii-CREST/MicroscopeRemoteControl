using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    public class ND_GetTimePhase : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_GetTimePhase()
            : base()
        {
            this.MacroName = "ND_GetTimePhase";
        }

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, false )]
        public int ND_TimeIndex;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;

    }
}
