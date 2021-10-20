using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    public class CameraGet_ImagingScannerWidth : NisMacroBase
    {
           /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGet_ImagingScannerWidth()
            : base()
        {
            this.MacroName = "CameraGet_ImagingScannerWidth";
        }

        /// <summary>
        /// スキャン幅
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
