using System;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// パラメータタイプ列挙
    /// </summary>
    public enum eParamType
    {
        /// <summary>
        /// NisElementsへ入力されるパラメータ
        /// </summary>
        InParam,
        /// <summary>
        /// NisElementsから出力されるパラメータ
        /// </summary>
        OutParam,
        /// <summary>
        /// 戻り値
        /// </summary>
        Return,
        /// <summary>
        /// 戻り値
        /// ※戻り値が定義されないマクロに使用する
        /// </summary>
        Return_Ignore
    }

    /// <summary>
    /// マクロパラメータアトリビュートクラス
    /// マクロへのパラメータの種類を定義する
    /// </summary>
    [AttributeUsage( AttributeTargets.Field, AllowMultiple = true )]
    public class NisMacroAttribute : Attribute
    {
        /// <summary>
        /// パラメータタイプ
        /// </summary>
        public eParamType ParamType;
        /// <summary>
        /// インデックス
        /// </summary>
        public int Index;

        /// <summary>
        /// ポインタ参照するかどうか
        /// </summary>
        public bool IsPointer;

        /// <summary>
        /// リストで取得する場合、サイズ
        /// </summary>
        public int ArraySize;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="paramType">パラメータタイプ</param>
        /// <param name="index">In、Outを含めたIndex番号 1オリジンとし、0はReturnとして定義する</param>
        public NisMacroAttribute( eParamType paramType, int index, bool isPointer = false )
        {
            this.ParamType = paramType;
            this.Index = index;
            this.IsPointer = isPointer;
        }
    }
}
