using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Experiment
{
    public class ND_RunExperiment : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_RunExperiment() : base()
        {
            this.MacroName = "ND_RunExperiment";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.EventDescription = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int EventDescription;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
