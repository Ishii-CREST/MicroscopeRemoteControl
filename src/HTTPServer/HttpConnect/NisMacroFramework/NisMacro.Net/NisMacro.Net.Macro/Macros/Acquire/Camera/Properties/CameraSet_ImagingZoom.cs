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
    public class CameraSet_ImagingZoom : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraSet_ImagingZoom() : base()
        {
            this.MacroName = "CameraSet_ImagingZoom";
        }

        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();

            this.Value = 1;
        }

        /// <summary>
        /// ズーム倍率値(1 ～ 1000)
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1 )]
        public double Value;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
