using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraSet_IRLasernWavelength
    /// </summary>
    public class CameraSet_IRLasernWavelength:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraSet_IRLasernWavelength(eIRLaser channel) : base()
        {
            this.MacroName = string.Format("CameraSet_IRLaser{0}Wavelength", (int)channel);
        }
        /// <summary>
        /// IR波長
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
