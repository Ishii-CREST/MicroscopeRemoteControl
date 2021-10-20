using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraGet_LineAveraging : NisAPIBase
    {

        #region method
		protected override bool RunAPIImpl()
        {
            
            try
            {
                
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_LineAveraging macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_LineAveraging();

                //CameraSet_ScanFrameRateIndex
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
