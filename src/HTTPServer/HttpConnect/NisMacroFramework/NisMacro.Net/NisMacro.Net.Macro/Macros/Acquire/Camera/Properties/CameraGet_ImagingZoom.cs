using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{

    /// <summary>
    /// スキャンズーム倍率変更
    /// </summary>
    public class CameraGet_ImagingZoom : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGet_ImagingZoom() : base()
        {
            this.MacroName = "CameraGet_ImagingZoom";
        }

        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();
        }

        /// <summary>
        /// ズーム倍率値(1 ～ 1000)
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;

    }
}
