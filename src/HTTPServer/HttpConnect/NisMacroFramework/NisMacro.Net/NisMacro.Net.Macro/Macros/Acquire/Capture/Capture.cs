using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Capture
{
    /// <summary>
    /// 撮像の開始要求
    /// </summary>
    public class Capture : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Capture() : base()
        {
            this.MacroName = "Capture";
        }
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
