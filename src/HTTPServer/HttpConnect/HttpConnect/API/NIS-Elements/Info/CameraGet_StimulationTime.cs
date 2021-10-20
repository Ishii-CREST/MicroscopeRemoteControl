using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraGet_StimulationTime : NisAPIBase
    {
        private enum eQueryIndex
        {
            stimID = 0,
            
        }

        protected override void DefineAPIParams()
        {
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
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_StimulationTimen macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_StimulationTimen
                    (NisSettingCommander.GetNisStimID(ParamValues[eQueryIndex.stimID].ToInt));

                //CameraSet_CHnLaserPowerの実行
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
