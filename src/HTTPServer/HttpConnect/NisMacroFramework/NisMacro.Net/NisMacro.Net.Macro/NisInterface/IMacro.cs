
using System.Collections.Generic;
using System.Reflection;

namespace NisMacro.Net.Macro.NisInterface
{
    /// <summary>
    /// マクロのインターフェース
    /// </summary>
    public interface IMacro
    {
        /// <summary>
        /// NisMacro属性を持つメンバを取得する
        /// </summary>
        /// <param name="paramTypes">パラメータ種別</param>
        /// <returns>フィールドメンバ情報</returns>
        List<ExMemberInfo> GetNisParamMemberInfo( params eParamType[] paramTypes );

        void SetNisParamMemberInfo( List<ExMemberInfo> newMemInfoList);

        /// <summary>
        /// マクロ名を取得する
        /// </summary>
        /// <returns>マクロ名</returns>
        string GetMacroName();

        /// <summary>
        /// NisMacro送信用のメッセージを作成する
        /// </summary>
        /// <returns>送信用文字列</returns>
        string CreateCmd();
    }
}
