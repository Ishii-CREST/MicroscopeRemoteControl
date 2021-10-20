using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    /// <summary>
    /// This function sets the type of the selected ROI. 
    /// Different ROI types are used for different purposes.
    /// </summary>
    public class ChangeROIType:NisMacroBase
    {
        public ChangeROIType() : base()
        {
            this.MacroName = "ChangeROIType";
        }

        /// <summary>
        /// ROI ID.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public int RoiId;

        /// <summary>
        /// Type of the ROI.
        /// 0:Standard
        /// 1:Background
        /// 2:Reference
        /// 3:Stimulation
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2)]
        public int RoiType;


        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
