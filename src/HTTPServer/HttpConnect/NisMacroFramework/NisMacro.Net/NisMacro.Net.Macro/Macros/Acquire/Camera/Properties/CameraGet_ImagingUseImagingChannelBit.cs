using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    public class CameraGet_ImagingUseImagingChannelBit : NisMacroBase
    {
         /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGet_ImagingUseImagingChannelBit()
            : base()
        {
            this.MacroName = "CameraGet_ImagingUseImagingChannelBit";
        }

        /// <summary>
        /// 使用Ch
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
