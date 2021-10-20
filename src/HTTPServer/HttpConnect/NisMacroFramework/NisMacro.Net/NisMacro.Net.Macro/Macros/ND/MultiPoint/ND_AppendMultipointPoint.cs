using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.MultiPoint
{
    /// <summary>
    /// This function appends a line to the current multipoint experiment table.
    /// </summary>
    public class ND_AppendMultipointPoint : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_AppendMultipointPoint()
            : base()
        {
            this.MacroName = "ND_AppendMultipointPoint";
        }

        /// <summary>
        /// The absolute X position of the new point (µm)
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public double ND_MultipointXCoord;
        /// <summary>
        /// The absolute Y position of the new point (µm)
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public double ND_MultipointYCoord;
        /// <summary>
        /// The absolute Z position of the new point (µm)
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public double ND_MultipointZCoord;
        /// <summary>
        /// The name of the variable which will contain the multipoint phase name.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 4, true)]
        public string ND_MultipointName;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;

    }
}
