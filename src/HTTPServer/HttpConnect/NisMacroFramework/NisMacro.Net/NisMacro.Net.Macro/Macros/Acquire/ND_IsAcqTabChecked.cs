using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire
{
    /// <summary>
    /// Returns true if the specified Tab of the ND Acquisition window is enabled.
    /// </summary>
    public class ND_IsAcqTabChecked:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_IsAcqTabChecked() : base()
        {
            this.MacroName = "ND_IsAcqTabChecked";
        }

        /// <summary>
        /// Name of an ND Acquisition tab as you see it in the dialog window (case sensitive).
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1,true)]
        public string NDAcquisitionTabName;

        /// <summary>
        /// 結果
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
