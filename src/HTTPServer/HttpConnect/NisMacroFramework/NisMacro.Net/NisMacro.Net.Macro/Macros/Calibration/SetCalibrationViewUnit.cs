using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Calibration
{
    /// <summary>
    /// SetCalibrationViewUnit
    /// </summary>
    public class SetCalibrationViewUnit : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetCalibrationViewUnit()
            : base()
        {
            this.MacroName = "SetCalibrationViewUnit";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        /// <summary>
        /// Name of the calibration unit.
        /// px:Pixels
        /// km:Kilometers
        /// m:Meters
        /// dm:Decimeters
        /// cm:Centimeters
        /// mm:Millimeters
        /// um:Micrometers
        /// nm:Nanometers
        /// inch:Inches
        /// mil:Mils
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, true)]
        public string CalibrationViewUnit;

        /// <summary>
        /// Result Not use.
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
