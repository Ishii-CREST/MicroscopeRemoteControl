using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Calibration
{
    /// <summary>
    /// GetCalibrationViewPrecision
    /// </summary>
    public class GetCalibrationViewPrecision : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetCalibrationViewPrecision()
            : base()
        {
            this.MacroName = "GetCalibrationViewPrecision";
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
        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public int CalibrationViewPrecision;

        /// <summary>
        /// Result Not use.
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
