using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{ 
    /// <summary>
    /// CameraSet_ScanFrameRateIndex
    /// </summary>
    public class CameraSet_ScanFrameRateIndex : NisMacroBase
    {
        public CameraSet_ScanFrameRateIndex():base()
        {
            this.MacroName = "CameraSet_ScanFrameRateIndex";
        }

        [NisMacroAttribute(eParamType.InParam,1)]
        public int CameraPropIntParam;

        [NisMacroAttribute(eParamType.Return_Ignore,0)]
        public int Result;

    }
}
