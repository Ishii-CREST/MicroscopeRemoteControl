using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraGet_ScanAreaAspect
    /// </summary>
    public class CameraGet_ScanAreaAspect : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGet_ScanAreaAspect()
            : base()
        {
            this.MacroName = "CameraGet_ScanAreaAspect";
        }

        /// <summary>
        /// LineAverage数
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 1, true )]
        public double Value;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
