using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Experiment
{
    /// <summary>
    /// ND_InsertEvent
    /// </summary>
    public class ND_InsertEvent : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_InsertEvent() : base()
        {
            this.MacroName = "ND_InsertEvent";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.EventDescription = string.Empty;
        }

        /// <summary>
        /// イベントタイプ(何らかのタグ値?）
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, false )]
        public int EventType;

        /// <summary>
        /// msec単位のタイムスタンプ
        /// 0を送ると現在撮像中のフレーム枚目をNIS上で指定する
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2, false )]
        public double EventTime;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 3, true )]
        public string EventDescription;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}