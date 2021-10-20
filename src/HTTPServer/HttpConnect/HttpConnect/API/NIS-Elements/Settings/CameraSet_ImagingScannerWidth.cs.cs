using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_ImagingScannerWidth : NisAPIBase
    {

        private enum eQueryIndex
        {
            width = 0,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.width,
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
                
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_ImagingScannerWidth macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_ImagingScannerWidth()
                    {
                        CameraPropIntParam = ParamValues[eQueryIndex.width].ToInt
                    };
                //CameraSet_ImagingScannerWidth
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
