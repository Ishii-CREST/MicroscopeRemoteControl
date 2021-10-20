using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ImageDocument.Save
{
    /// <summary>
    /// カレント画像を保存する
    /// </summary>
    public class ImageSaveAs : NisMacroBase
    {
        /// <summary>
        /// 保存タイプ
        /// </summary>
        public enum eImType : int
        {
            /// <summary>
            /// バイナリ画像のみ
            /// </summary>
            Binary = 1,
            /// <summary>
            /// カラー画像のみ
            /// </summary>
            Color = 2,
            /// <summary>
            /// ベクターのみ
            /// </summary>
            Vector = 8,
            /// <summary>
            /// 全レイヤー
            /// </summary>
            AllLayer = 15
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageSaveAs()
            : base()
        {
            this.MacroName = "ImageSaveAs";
        }

        /// <summary>
        /// デフォルトの値を設定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            base.SetDefaultParamater();

            this.ImType = (int)eImType.AllLayer;
            this.ImCompr = 0;
        }

        /// <summary>
        /// 保存先(拡張子含む)
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, true )]
        public string Image;

        /// <summary>
        /// 保存タイプ
        /// 1   = Binary image
        /// 2   = Color image
        /// 8   = Vector annotations
        /// 15  = All layers
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2 )]
        public int ImType;

        /// <summary>
        /// 圧縮率
        /// 詳細はマクロのヘルプを参照
        /// 通常 0 でよさそう
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 3 )]
        public int ImCompr;

        /// <summary>
        /// 本マクロでは未使用
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
