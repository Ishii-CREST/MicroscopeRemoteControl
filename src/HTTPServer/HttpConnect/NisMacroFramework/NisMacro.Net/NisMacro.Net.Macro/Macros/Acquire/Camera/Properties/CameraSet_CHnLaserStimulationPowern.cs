using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// 刺激用レーザーパワーを設定する。
    /// </summary>
    public class CameraSet_CHnLaserStimulationPowern : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        /// <param name="stimID">刺激ID</param>
        public CameraSet_CHnLaserStimulationPowern(eChanncel channel, eStimID stimID) : base()
        {
            this.MacroName = string.Format("CameraSet_CH{0}LaserStimulationPower{1}", (int)channel, (int)stimID);
        }

        /// <summary>
        /// レーザパワー(0 ～ 100)
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 1)]
        public double Value;

        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute(eParamType.Return_Ignore, 0)]
        public int Result;
    }
}
