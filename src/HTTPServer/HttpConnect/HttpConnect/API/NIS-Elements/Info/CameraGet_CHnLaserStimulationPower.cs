using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraGet_CHnLaserStimulationPower : NisAPIBase
    {
        private enum eQueryIndex
        {
            Channel = 0,
            stimID,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.Channel,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH4
                });
            ParamValues.Add(eQueryIndex.stimID,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim3
                });
            CreateParamList(typeof(eQueryIndex));
        }


        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                

                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_CHnLaserStimulationPowern macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_CHnLaserStimulationPowern
                    (NisSettingCommander.GetNisCh(ParamValues[eQueryIndex.Channel].ToInt),
                    NisSettingCommander.GetNisStimID(ParamValues[eQueryIndex.stimID].ToInt));

                //CameraGet_CHnLaserStimulationPower
                AddMacroToQueSync(macro);

                HttpResponse.ResponseParam.Add(macro.Value);
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                OutputLog("", e);
                return false;
            }
        }
        #endregion
    }
}
