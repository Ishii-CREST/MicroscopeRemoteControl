using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Experiment
{
    /// <summary>
    /// ND_DefineExperiment
    /// </summary>
    public class ND_DefineExperiment: NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_DefineExperiment() : base()
        {
            this.MacroName = "ND_DefineExperiment";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }
        
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int ND_TExp;

        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public int ND_XYExp;

        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public int ND_ZExp;

        [NisMacroAttribute(eParamType.InParam, 4, false)]
        public int ND_LExp;

        [NisMacroAttribute(eParamType.InParam, 5, false)]
        public int ND_NormalZOrder;

        [NisMacroAttribute(eParamType.InParam, 6, true)]
        public string Filename;

        [NisMacroAttribute(eParamType.InParam, 7, true)]
        public string Prefix;

        [NisMacroAttribute(eParamType.InParam, 8, false)]
        public int ND_FileType;

        [NisMacroAttribute(eParamType.InParam, 9, false)]
        public int ND_UseTIFF;

        [NisMacroAttribute(eParamType.InParam, 10, false)]
        public int ND_UsePFS;

        [NisMacroAttribute(eParamType.InParam, 11, false)]
        public int ND_EnableTMeas;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
