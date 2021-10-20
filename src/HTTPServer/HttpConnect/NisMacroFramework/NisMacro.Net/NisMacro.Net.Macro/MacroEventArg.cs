using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NisMacro.Net.Macro.NisInterface;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// マクロコマンドイベントクラス
    /// </summary>
    public class MacroEventArgs : EventArgs
    {
        /// <summary>
        /// タイムアウトしたかどうか
        /// </summary>
        public bool IsTimeOut
        {
            get;
            private set;
        }

        /// <summary>
        /// マクロオブジェクト
        /// </summary>
        public IMacro Imacro
        {
            get;
            private set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="iMacro">マクロオブジェクト</param>
        /// <param name="isTimeOut">タイムアウトしたかどうか</param>
        public MacroEventArgs( IMacro iMacro, bool isTimeOut = false )
        {
            this.IsTimeOut = isTimeOut;
            this.Imacro = iMacro;
        }
    }
}
