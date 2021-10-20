using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    public class ND_GetTimePhaseSchedule : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_GetTimePhaseSchedule()
            : base()
        {
            this.MacroName = "ND_GetTimePhaseSchedule";
        }

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, false )]
        public int ND_TimePhase;

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 2, true)]
        public double ND_GetTimeInterval;

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 3, true)]
        public double ND_GetTimeDuration;

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 4, true)]
        public int ND_TimeLoopCnt;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;

    }
}
