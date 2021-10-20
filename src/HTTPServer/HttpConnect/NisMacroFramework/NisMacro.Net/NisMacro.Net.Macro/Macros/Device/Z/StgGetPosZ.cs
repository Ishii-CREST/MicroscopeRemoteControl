using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Z
{
    /// <summary>
    /// StgGetPosZ
    /// </summary>
    public class StgGetPosZ : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StgGetPosZ()
            : base()
        {
            this.MacroName = "StgGetPosZ";
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
        public double LpStgZ;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2, false )]
        public int ZDevice;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
