using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CH1で指定されているレーザパワーを取得する
    /// </summary>
    public class CameraGet_CHnLaserStimulationPowern : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        /// <param name="stimID">刺激ID</param>
        public CameraGet_CHnLaserStimulationPowern(eChanncel channel,eStimID stimID) : base()
        {
            this.MacroName = string.Format("CameraGet_CH{0}LaserStimulationPower{1}", (int)channel, (int)stimID);
        }

        /// <summary>
        /// レーザパワー(0 ～ 100)
        /// </summary>
        [NisMacroAttribute(eParamType.OutParam, 1, true)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
