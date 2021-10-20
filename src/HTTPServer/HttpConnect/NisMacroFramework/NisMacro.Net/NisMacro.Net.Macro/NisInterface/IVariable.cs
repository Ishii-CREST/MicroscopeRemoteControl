using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.NisInterface
{
    /// <summary>
    /// Nis変数インターフェース
    /// </summary>
    public interface IVariable
    {
        /// <summary>
        /// NisMacro送信用のメッセージを作成する
        /// </summary>
        /// <returns>送信用文字列</returns>
        string CreateSetCmd();

        /// <summary>
        /// NisMacro送信用のメッセージを作成する
        /// </summary>
        /// <returns>送信用文字列</returns>
        string CreateGetCmd();

        /// <summary>
        /// タイプ取得
        /// </summary>
        /// <returns></returns>
        Type GetVariableType();

        /// <summary>
        /// 値取得
        /// </summary>
        /// <returns></returns>
        object GetValue();

        /// <summary>
        /// 値設定
        /// </summary>
        /// <param name="val">値</param>
        /// <returns></returns>
        void SetValue( object val );
    }
}
