using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.API.APIValue;
namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_CHnLaserPower : NisAPIBase
    {
        private enum eQueryIndex
        {
            channel = 0,
            power,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.channel,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eChanncel.CH4
                });
            ParamValues.Add(eQueryIndex.power,
                new DoubleValue()
                {
                    MinValue = 0,
                    MaxValue = 100
                });

            CreateParamList(typeof(eQueryIndex));
        }

        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_CHnLaserPower macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_CHnLaserPower
                    (NisSettingCommander.GetNisCh(ParamValues[eQueryIndex.channel].ToInt))
                    {
                        Value = ParamValues[eQueryIndex.power].ToDouble
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
