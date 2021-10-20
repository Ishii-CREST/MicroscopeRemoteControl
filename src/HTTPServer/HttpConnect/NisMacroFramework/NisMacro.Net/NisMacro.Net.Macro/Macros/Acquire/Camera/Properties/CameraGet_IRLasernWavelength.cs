using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraGet_IRLasernWavelength
    /// </summary>
    public class CameraGet_IRLasernWavelength:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraGet_IRLasernWavelength(eIRLaser channel) : base()
        {
            this.MacroName = string.Format("CameraGet_IRLaser{0}Wavelength", (int)channel);
        }
        /// <summary>
        /// IR波長
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public int Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
