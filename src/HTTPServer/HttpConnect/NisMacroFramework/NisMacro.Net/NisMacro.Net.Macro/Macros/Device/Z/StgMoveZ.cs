using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Z
{
    /// <summary>
    /// StgMoveZ
    /// </summary>
    public class StgMoveZ : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StgMoveZ()
            : base()
        {
            this.MacroName = "StgMoveZ";
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
        public double stgZ;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public int relative;


        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
