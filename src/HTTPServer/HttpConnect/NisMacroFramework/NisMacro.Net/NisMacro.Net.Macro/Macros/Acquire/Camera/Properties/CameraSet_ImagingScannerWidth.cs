using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraSet_ImagingScannerWidth
    /// </summary>
    public class CameraSet_ImagingScannerWidth : NisMacroBase
    {
        public CameraSet_ImagingScannerWidth():base()
        {
            this.MacroName = "CameraSet_ImagingScannerWidth";
        }

        [NisMacroAttribute(eParamType.InParam,1)]
        public int CameraPropIntParam;

        [NisMacroAttribute(eParamType.Return_Ignore,0)]
        public int Result;
    }
}
