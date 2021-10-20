using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_ImagingZoom : NisAPIBase
    {
        private enum eQueryIndex
        {
            Zoom = 0,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.Zoom,
                new DoubleValue()
                {
                    MinValue = 0,
                    MaxValue = double.MaxValue
                });
            CreateParamList(typeof(eQueryIndex));
        }


        #region method
        protected override bool RunAPIImpl()
        {
            
            try
            {
                
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_ImagingZoom macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_ImagingZoom()
                {
                     Value = ParamValues[eQueryIndex.Zoom].ToDouble
                };
                //CameraGet_ImagingZoom
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
