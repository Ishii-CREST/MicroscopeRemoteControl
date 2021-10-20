using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Nosepiece
{
    /// <summary>
    /// 対物レンズ台位置取得
    /// </summary>
    public class Stg_GetNosepiecePosition : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Stg_GetNosepiecePosition() : base()
        {
            this.MacroName = "Stg_GetNosepiecePosition";
        }


        /// <summary>
        /// 対物レンズのインデックス
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
