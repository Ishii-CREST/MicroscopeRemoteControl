using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.AdvancedAPI.InterProcess
{
    /// <summary>
    /// Int_ExecuteCommand
    /// </summary>
    public class Int_ExecuteCommand : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Int_ExecuteCommand(): base()
        {
            this.MacroName = "Int_ExecuteCommand";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.Command = string.Empty;
        }

        /// <summary>
        ///Application ID of instance that will run the command.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public int AppId;
        /// <summary>
        /// Command to run.
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2, true )]
        public string Command;

        /// <summary>
        /// Returns the resulting code of the command.
        /// </summary>
        [NisMacroAttribute( eParamType.Return, 0 )]
        public int Result;
    }
}
