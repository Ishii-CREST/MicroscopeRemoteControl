using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ROI
{
    public class GetROIInfo : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetROIInfo() : base()
        {
            this.MacroName = "GetROIInfo";
        }

        /// <summary>
        /// デフォルトパラメータ
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();
        }

        /// <summary>
        /// ROI ID.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public int RoiId;

        /// <summary>
        /// Left bounding box coordinate.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 2, true)]
        public int LpBBoxL;

        /// <summary>
        /// Top bounding box coordinate.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 3, true)]
        public int LpBBoxT;

        /// <summary>
        /// Right bounding box coordinate.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 4, true)]
        public int LpBBoxR;

        /// <summary>
        /// Bottom bounding box coordinate.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 5, true)]
        public int LpBBoxB;

        /// <summary>
        /// X coordinate of the Center.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 6, true)]
        public int LpCenterX;

        /// <summary>
        /// Y coordinate of the Center.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 7, true)]
        public int LpCenterY;

        /// <summary>
        /// If the ROI was an ellipse it is the minor axis.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 8, true)]
        public int LpMinFeret;

        /// <summary>
        /// If the ROI was an ellipse it is the major axis.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 9, true)]
        public int LpMaxFeret;

        /// <summary>
        /// The Orientation of the main axis (along MaxFerret). This value can be directly used in CreateEllipseROI.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 10, true)]
        public double LpRotation;

        /// <summary>
        /// RGB color of the ROI.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 11, true)]
        public long lpColorRGB;


        /// <summary>
        /// 結果
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
