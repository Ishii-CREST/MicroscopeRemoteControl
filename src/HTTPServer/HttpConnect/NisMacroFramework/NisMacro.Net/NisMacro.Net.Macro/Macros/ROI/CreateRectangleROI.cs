using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    /// <summary>
    /// Creates a new rectangle ROI and returns its ID.
    /// </summary>
    public class CreateRectangleROI:NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CreateRectangleROI() : base()
        {
            this.MacroName = "CreateRectangleROI";
        }

        /// <summary>
        /// デフォルトパラメータ
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();
        }

        /// <summary>
        /// X coordinate of the center.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int CenterX;

        /// <summary>
        /// Y coordinate of the center.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public int CenterY;

        /// <summary>
        /// Image width in pixels.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public int Width;

        /// <summary>
        /// Image height in pixels.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 4, false)]
        public int Height;

        /// <summary>
        /// Orientation of the X axis (width) of the rectangle.
        /// The angle is in degrees rising clockwise.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 5, false)]
        public double AngleRot;

        /// <summary>
        ///The color parameter specifies the color of the ROI
        ///. Use predefined color constants
        ///(RGB_BLUE (16711680), RGB_CYAN (16776960),
        ///RGB_GREEN (65280), RGB_MAGENTA (16711935),
        ///RGB_RED (255), RGB_WHITE (16777215),
        ///RGB_BLACK (0), RGB_YELLOW (65535) or
        ///RGB_DEFAULT (2147483647)) or any RGB value.
        ///See RGB function for details.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 6, false)]
        public long ColorRGB;

        /// <summary>
        /// The function returns ID of a newly created ROI.
        /// >0 ROI ID
        /// <0 The function failed
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
