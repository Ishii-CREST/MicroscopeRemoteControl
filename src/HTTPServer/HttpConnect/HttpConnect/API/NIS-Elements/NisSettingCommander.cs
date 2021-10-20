using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API
{
    public static  class NisSettingCommander
    {
        #region method
        /// <summary>
        /// NISChに変換します
        /// </summary>
        /// <param name="channel">設定チャンネル(1～4)</param>
        /// <returns>NIS　Ch</returns>
        public static NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel GetNisCh(int channel)
        {
            switch (channel)
            {
                case 1:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH1;
                case 2:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH2;
                case 3:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH3;
                case 4:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH4;
                default:
                    throw new Exception(string.Format("Channel is invalid. arg[{0}]", channel));
            }
        }

        /// <summary>
        /// NIS IR　IDに変換します
        /// </summary>
        /// <param name="IR"></param>
        /// <returns></returns>
        public static NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser GetNisIRID(int IR)
        {
            switch (IR)
            {
                case 1:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser1;
                case 2:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser2;
                default:
                    throw new Exception(string.Format("Channel is invalid. arg[{0}]", IR));
            }
        }

        /// <summary>
        ///　NIS　刺激IDに変換します
        /// </summary>
        /// <param name="stim"></param>
        /// <returns></returns>
        public static NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID GetNisStimID(int stim)
        {
            switch (stim)
            {
                case 1:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim1;
                case 2:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim2;
                case 3:
                    return NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim3;
                default:
                    throw new Exception(string.Format("Channel is invalid. arg[{0}]", stim));
            }
        }

        #endregion
    }
}
