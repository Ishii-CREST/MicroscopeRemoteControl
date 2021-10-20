using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Device.Nosepiece
{
    /// <summary>
    /// 対物レンズ名取得
    /// </summary>
    public class Stg_GetNosepieceObjectiveName : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Stg_GetNosepieceObjectiveName() : base()
        {
            this.MacroName = "Stg_GetNosepieceObjectiveName";
            this.SetDefaultParamater();
        }

        /// <summary>
        /// パラメータのデフォルト値を指定する
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();

            //  OutParamとして返される文字列の長さを指定する
            this.MaxLength = Define.STR_BUFFER_LENGTH;
        }

        /// <summary>
        /// 結果パラメータ
        /// 詳細はヘルプを参照
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;

        /// <summary>
        /// 取得したい対物レンズのレボルバーインデックス
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1 )]
        public int NosepiecePosition;

        /// <summary>
        /// 対物レンズ名
        /// </summary>
        [NisMacroAttribute( eParamType.OutParam, 2 )]
        public string Name;

        /// <summary>
        /// Nameで返される予定のバッファサイズ = 1024で固定する
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 3 )]
        public int MaxLength;
    }
}
