using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.Acquire.Camera.Properties
{
    /// <summary>
    /// CH1で指定されているレーザパワーを設定する
    /// </summary>
    public class CameraSet_CHnLaserPower : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channel">チャンネル</param>
        public CameraSet_CHnLaserPower( eChanncel channel )  : base()
        {
            this.MacroName = string.Format( "CameraSet_CH{0}LaserPower", (int)channel );
        }

        /// <summary>
        /// レーザパワー(1 ～ 100)
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1 )]
        public double Value;
        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
