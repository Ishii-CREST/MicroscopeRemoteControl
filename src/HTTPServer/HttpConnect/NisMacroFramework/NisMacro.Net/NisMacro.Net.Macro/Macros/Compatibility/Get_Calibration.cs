using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Compatibility
{
    /// <summary>
    /// Get_Calibration
    /// </summary>
    public class Get_Calibration : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Get_Calibration()
            : base()
        {
            this.MacroName = "Get_Calibration";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        /// <summary>
        /// Objective name to be filled.
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 1, true )]
        public string ObjectiveName;

        /// <summary>
        /// Size of one pixel in the specified units.
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 2, true)]
        public double cal;

        /// <summary>
        /// Aspect ratio of the image.
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 3, true)]
        public double Aspect;

        /// <summary>
        /// Points to calibration unit. Filled on output
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 4, true)]
        public int unit;


        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public double Result;
    }
}
