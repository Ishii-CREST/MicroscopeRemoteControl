using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ImageDocument.Activate
{
    /// <summary>
    /// アクティブ画像を切り替える
    /// </summary>
    public class ActivateDocument : NisMacroBase
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public ActivateDocument() : base()
        {
            this.MacroName = "ActivateDocument";
        }

        /// <summary>
        /// 画像パス
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, true )]
        public string DocumentName;

        /// <summary>
        /// 結果 このマクロでは使用しない
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
