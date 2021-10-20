using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.ZSeries
{
    /// <summary>
    /// The ND_SetZSeriesExp function enables to set parameters of the 
    /// current Z-series acquisition setting in the ND Acquisition dialog box.
    /// </summary>
    public class ND_SetZSeriesExp : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_SetZSeriesExp()
            : base()
        {
            this.MacroName = "ND_SetZSeriesExp";
        }

        /// <summary>
        /// Defines type of capturing frames in Z-Series.
        /// 0:Bottom-Top mode.
        /// 2:Mode with symmetrical range.
        /// 3:Mode with asymmetrical range.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1, false)]
        public int ND_LpZSeriesType;
        /// <summary>
        /// Defines absolute top range value in µm
        /// (in case of Bottom-Top type), or relative top 
        /// range above in µm (for range type) - 
        /// same as top yellow box at ZStack Cube.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 2, false)]
        public double ND_LpZSeriesTop;
        /// <summary>
        /// Defines absolute home position in µm 
        /// (applicable only in range type experiments with ZSeriesHomeDefined = 1).
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 3, false)]
        public double ND_LpZSeriesHome;
        /// <summary>
        /// Defines absolute bottom range value in µm 
        /// (in case of Bottom-Top type), or relative 
        /// bottom range below in µm (range type) - same as 
        /// bottom yellow box at ZStack Cube.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 4, false)]
        public double ND_LpZSeriesBottom;
        /// <summary>
        /// Defines the step between Z slices in µm.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 5, false)]
        public double ND_LpZSeriesStep;
        /// <summary>
        /// Defines number of Z slices. If equals zero, 
        /// it is ignored ; for positive values, 
        /// the range of experiment is changed according to the step size.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 6, false)]
        public int ND_LpZSeriesCount;
        /// <summary>
        /// This parameter defines absolute home position.
        /// If equals zero, ZSeriesHome is ignored and 
        /// range experiments are relative.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 7, false)]
        public int ND_LpZSeriesHomeDefined;
        /// <summary>
        /// Defines state of the "Close active Shutter during Z Movement" checkbox.
        /// 1:Close Shutter is enabled
        /// 0:Close Shutter is disabled
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 8, false)]
        public int ND_LpZSeriesCloseShuter;
        /// <summary>
        /// Defines name of the Z drive device
        /// (from combobox in ZStack Cube). If it is blank, 
        /// device is not set.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 9, true)]
        public string ND_ZSeriesDevice;
        /// <summary>
        /// A macro command to be performed before each loop.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 10, true)]
        public string ND_TimeCommandBefore;
        /// <summary>
        /// A macro command to be performed after each loop.
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 11, true)]
        public string ND_TimeCommandAfter;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
