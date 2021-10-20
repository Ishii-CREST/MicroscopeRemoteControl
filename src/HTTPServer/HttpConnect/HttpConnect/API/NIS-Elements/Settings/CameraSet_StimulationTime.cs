using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_StimulationTime : NisAPIBase
    {
        private enum eQueryIndex
        {
            stimID = 0,
            time,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.stimID,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eStimID.Stim3,
                });
            ParamValues.Add(eQueryIndex.time,
                new DoubleValue()
                {
                    MinValue = 0,
                });
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_StimulationTimen macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_StimulationTimen
                    (NisSettingCommander.GetNisStimID(ParamValues[eQueryIndex.stimID].ToInt))
                    {
                        Value = ParamValues[eQueryIndex.time].ToDouble
                    };

                //CameraSet_CHnLaserPowerの実行
                AddMacroToQueSync(macro);
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
