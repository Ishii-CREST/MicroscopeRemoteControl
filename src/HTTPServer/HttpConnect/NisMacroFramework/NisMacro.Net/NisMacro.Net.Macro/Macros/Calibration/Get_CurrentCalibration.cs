using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Calibration
{
    public class Get_CurrentCalibration : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Get_CurrentCalibration()
            : base()
        {
            this.MacroName = "Get_CurrentCalibration";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        /// <summary>
        /// Calibration of what camera mode shall be retrieved?
        /// 0:The currently active mode.
        /// 1:Fast camera mode
        /// 2:Quality camera mode
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int FQMode;

        /// <summary>
        /// In case of a dual camera system, select the camera 
        /// for which to retrieve the calibration.
        /// 0:The primary camera.
        /// 1:The secondary camera.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public int ActiveCamera;
        /// <summary>
        /// Objective name to be filled.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 3, true)]
        public string ObjectiveName;

        /// <summary>
        /// Size of one pixel in the specified units.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 4, true)]
        public double cal;

        /// <summary>
        /// Aspect ratio of the image.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 5, true)]
        public double Aspect;

        /// <summary>
        /// Points to calibration unit. Filled on output
        /// 0:Pixels
        /// 1:Nanometers
        /// 2:Micrometers
        /// 3:Millimeters
        /// 4:Centimeters
        /// 5:Decimeters
        /// 6:Meters
        /// 7:Kilometers
        /// 8:Mils
        /// 9:Inches
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 6, true)]
        public int unit;


        /// <summary>
        /// This function returns calibration of the selected 
        /// camera mode - the LpCal parameter value.
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public double Result;
    }
}
