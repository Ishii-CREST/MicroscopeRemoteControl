using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.ZSeries
{
    public class ND_GetZSeriesExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_GetZSeriesExp()
            : base()
        {
            this.MacroName = "ND_GetZSeriesExp";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
        }

        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public int ND_LpZSeriesType;

        [NisMacroAttribute(eParamType.OutParam, 2, true)]
        public double ND_LpZSeriesTop;

        [NisMacroAttribute(eParamType.OutParam, 3, true)]
        public double ND_LpZSeriesHome;

        [NisMacroAttribute(eParamType.OutParam, 4, true)]
        public double ND_LpZSeriesBottom;

        [NisMacroAttribute(eParamType.OutParam, 5, true)]
        public double ND_LpZSeriesStep;

        [NisMacroAttribute(eParamType.OutParam, 6, true)]
        public int ND_LpZSeriesCount;

        [NisMacroAttribute(eParamType.OutParam, 7, true)]
        public int ND_LpZSeriesHomeDefined;

        [NisMacroAttribute(eParamType.OutParam, 8, true)]
        public int ND_LpZSeriesCloseShuter;

        [NisMacroAttribute(eParamType.OutParam, 9, true)]
        public string ND_ZSeriesDevice;

        [NisMacroAttribute(eParamType.OutParam, 10, true)]
        public string ND_TimeCommandBefore;

        [NisMacroAttribute(eParamType.OutParam, 11, true)]
        public string ND_TimeCommandAfter;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return, 0)]
        public int Result;
    }
}
