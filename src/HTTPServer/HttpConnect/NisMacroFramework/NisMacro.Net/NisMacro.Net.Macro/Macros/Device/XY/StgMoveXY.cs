using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.XY
{
    /// <summary>
    /// StgMoveXY
    /// </summary>
    public class StgMoveXY : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StgMoveXY()
            : base()
        {
            this.MacroName = "StgMoveXY";
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
        [NisMacroAttribute( eParamType.InParam, 1, false )]
        public double stgX;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2, false )]
        public double stgY;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public int relative;


        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
