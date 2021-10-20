using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ImageDocument.Close
{
    /// <summary>
    /// カレントの画像を閉じる
    /// </summary>
    public class CloseCurrentDocument : NisMacroBase
    {
        /// <summary>
        /// Save値列挙
        /// </summary>
        public enum eSave : int
        {
            /// <summary>
            /// Cancel Operation.
            /// </summary>
            QUERYSAVE_ASK = 0,
            /// <summary>
            /// Changes are saved.
            /// </summary>
            QUERYSAVE_YES = 1,
            /// <summary>
            /// Changes are not saved.
            /// </summary>
            QUERYSAVE_NO = 2
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CloseCurrentDocument() : base()
        {
            this.MacroName = "CloseCurrentDocument";
        }

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;

        /// <summary>
        /// 変更時のダイアログ表示
        /// 0 = QUERYSAVE_ASK   Cancel Operation.
        /// 1 = QUERYSAVE_YES   Changes are saved.
        /// 2 = QUERYSAVE_NO    Changes are not saved.
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1 )]
        public int Save;
    }
}
