using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Calibration
{
    public class SetCalibrationViewPrecision : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetCalibrationViewPrecision()
            : base()
        {
            this.MacroName = "SetCalibrationViewPrecision";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        /// <summary>
        /// Number of digits after the decimal point.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int CalibrationViewPrecision;

        /// <summary>
        /// Result Not use.
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
