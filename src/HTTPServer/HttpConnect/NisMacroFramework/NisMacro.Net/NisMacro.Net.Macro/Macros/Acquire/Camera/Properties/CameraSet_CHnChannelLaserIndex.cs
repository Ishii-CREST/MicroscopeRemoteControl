using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CameraSet_CHnChannelLaserIndex
    /// </summary>
    public class CameraSet_CHnChannelLaserIndex :NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraSet_CHnChannelLaserIndex(eChanncel channel) : base()
        {
            this.MacroName = string.Format("CameraSet_CH{0}ChannelLaserIndex", (int)channel);
        }

        /// <summary>
        /// レーザIndex
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
