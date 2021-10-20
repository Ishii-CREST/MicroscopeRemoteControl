using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraGet_IRLasernWavelength : NisAPIBase
    {
        private enum eQueryIndex
        {
            IR = 0,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.IR,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser2
                });
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                //対象チャンネル作成指示
                //APIElements.SetLaser();
                

                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_IRLasernWavelength macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_IRLasernWavelength
                    (NisSettingCommander.GetNisIRID(ParamValues[eQueryIndex.IR].ToInt));


                //CameraGet_IRLasernWavelength
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
