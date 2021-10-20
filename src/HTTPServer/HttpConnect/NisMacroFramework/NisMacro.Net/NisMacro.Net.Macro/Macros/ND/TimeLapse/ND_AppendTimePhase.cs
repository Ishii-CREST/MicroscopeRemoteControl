using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.TimeLapse
{
    public class ND_AppendTimePhase : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_AppendTimePhase()
            : base()
        {
            this.MacroName = "ND_AppendTimePhase";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.ND_TimeCommand = string.Empty;
        }
        
        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, false )]
        public double ND_TimeInterval;

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public double ND_TimeDuration;

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, true)]
        public string ND_TimeCommand;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
