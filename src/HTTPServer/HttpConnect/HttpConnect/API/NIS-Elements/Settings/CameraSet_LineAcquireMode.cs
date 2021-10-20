using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CameraSet_LineAcquireMode : NisAPIBase
    {
        private enum eQueryIndex
        {
            AcquireMode = 0,            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.AcquireMode,new IntValue());        //　範囲は設けない
            CreateParamList(typeof(eQueryIndex));
        }


        #region method
        protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_LineAcquireMode macro
                    = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraSet_LineAcquireMode()
                {
                      CameraPropIntParam = ParamValues[eQueryIndex.AcquireMode].ToInt
                };
                //CameraSet_LineAcquireMode
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
