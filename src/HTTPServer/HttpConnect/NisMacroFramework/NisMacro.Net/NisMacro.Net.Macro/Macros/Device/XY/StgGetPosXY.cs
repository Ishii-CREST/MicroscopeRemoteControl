using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.XY
{
    /// <summary>
    /// StgGetPosXY
    /// </summary>
    public class StgGetPosXY : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StgGetPosXY()
            : base()
        {
            this.MacroName = "StgGetPosXY";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 1, true )]
        public double LpStgX;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 2, true )]
        public double LpStgY;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
