using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class GetROIInfo : NisAPIBase
    {
        private enum eQueryIndex
        {
            RoiID = 0,
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.RoiID,
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
                NisMacro.Net.Macro.Macros.ROI.GetROIInfo macro
                    = new NisMacro.Net.Macro.Macros.ROI.GetROIInfo() {
                        RoiId = ParamValues[eQueryIndex.RoiID].ToInt
                    };

                //GetROIInfo
                AddMacroToQueSync(macro);

                HttpResponse.ResponseParam.Add(macro.LpBBoxL);
                HttpResponse.ResponseParam.Add(macro.LpBBoxT);
                HttpResponse.ResponseParam.Add(macro.LpBBoxR);
                HttpResponse.ResponseParam.Add(macro.LpBBoxB);
                HttpResponse.ResponseParam.Add(macro.LpCenterX);
                HttpResponse.ResponseParam.Add(macro.LpCenterY);
                HttpResponse.ResponseParam.Add(macro.LpMinFeret);
                HttpResponse.ResponseParam.Add(macro.LpMaxFeret);
                HttpResponse.ResponseParam.Add(macro.LpRotation);
                HttpResponse.ResponseParam.Add(macro.lpColorRGB);
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
