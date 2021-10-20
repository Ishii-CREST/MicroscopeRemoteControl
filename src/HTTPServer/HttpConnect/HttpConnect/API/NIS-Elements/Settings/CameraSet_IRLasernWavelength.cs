using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_IRLasernWavelength : NisAPIBase
    {
        private enum eQueryIndex
        {
            IR = 0,
            wavelength,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.IR,
                new IntValue()
                {
                    MinValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser1,
                    MaxValue = (int)NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.eIRLaser.Laser2
                });
            ParamValues.Add(eQueryIndex.wavelength,
                new DoubleValue());
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_IRLasernWavelength macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_IRLasernWavelength
                    (NisSettingCommander.GetNisIRID(ParamValues[eQueryIndex.IR].ToInt))
                    {
                        Value = ParamValues[eQueryIndex.wavelength].ToDouble
                    };

                //CameraSet_IRLasernWavelength
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
