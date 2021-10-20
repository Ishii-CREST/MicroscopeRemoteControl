using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraGet_LineAveraging
    /// </summary>
    public class CameraGet_LineAveraging:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGet_LineAveraging()
            : base()
        {
            this.MacroName = "CameraGet_LineAveraging";
        }

        /// <summary>
        /// LineAverage数
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 1, true )]
        public int Value;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
