using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_CHnChannelLaserIndex : NisAPIBase
    {
        private enum eQueryIndex:int
        {
            channel,
            laserIndex,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.channel,
                new IntValue()
            {
                MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH1,
                MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH4
            });
            ParamValues.Add(eQueryIndex.laserIndex,
                new IntValue()
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
                
                
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_CHnChannelLaserIndex macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_CHnChannelLaserIndex(
                        NisSettingCommander.GetNisCh(ParamValues[eQueryIndex.channel].ToInt))
                    {
                        Value = ParamValues[eQueryIndex.laserIndex].ToInt
                    };

                //CHnChannelLaserIndexの実行
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
