using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Nosepiece
{
    /// <summary>
    /// 対物レンズ位置セット
    /// </summary>
    public class Stg_SetNosepiecePosition : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Stg_SetNosepiecePosition() : base()
        {
            this.MacroName = "Stg_SetNosepiecePosition";
        }

        /// <summary>
        /// 対物レンズのインデックス
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1 )]
        public int ObjIndex;

        /// <summary>
        /// DR_OK (1):Nosepiece objective position was set
        /// DR_BADPARAMETER (-2):Some parameter has invalid value
        /// DR_NOTAVAILABLE (-4):Device Nosepiece is not present
        /// DR_UNKNOWNERROR (-1):Setting nosepiece objective position failed
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
