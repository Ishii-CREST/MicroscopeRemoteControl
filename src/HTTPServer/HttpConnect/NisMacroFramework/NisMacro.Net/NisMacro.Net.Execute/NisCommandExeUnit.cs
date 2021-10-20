using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Execute
{
    /// <summary>
    /// NISの実行単位。内部専用クラス。
    /// </summary>
    public class NisCommandExeUnit
    {
        /// <summary>
        /// 実行するマクロ
        /// </summary>
        public NisMacro.Net.Macro.NisInterface.IMacro exeMacro { get; private set; }
        /// <summary>
        /// 完了フラグ
        /// </summary>
        public bool isCompete;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="macro"></param>
        public NisCommandExeUnit(NisMacro.Net.Macro.NisInterface.IMacro macro)
        {
            exeMacro = macro;
        }
    }
}
