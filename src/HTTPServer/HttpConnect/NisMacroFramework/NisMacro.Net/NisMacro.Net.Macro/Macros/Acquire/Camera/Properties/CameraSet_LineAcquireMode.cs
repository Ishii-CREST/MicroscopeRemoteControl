using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraSet_LineAcquireMode
    /// </summary>
    public class CameraSet_LineAcquireMode : NisMacroBase
    {
        public CameraSet_LineAcquireMode():base()
        {
            this.MacroName = "CameraSet_LineAcquireMode";
        }

        [NisMacroAttribute(eParamType.InParam,1)]
        public int CameraPropIntParam;

        [NisMacroAttribute(eParamType.Return_Ignore,0)]
        public int Result;
    }
}
