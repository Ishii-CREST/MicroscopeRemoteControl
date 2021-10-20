using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND
{
    public class NDEnableSaveToTIFF: NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NDEnableSaveToTIFF()
            : base()
        {
            this.MacroName = "NDEnableSaveToTIFF";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.ND_EnableSaveToTIFF = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int ND_EnableSaveToTIFF;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
