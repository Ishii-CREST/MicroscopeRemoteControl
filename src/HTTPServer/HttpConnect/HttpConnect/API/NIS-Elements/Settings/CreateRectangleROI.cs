using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class CreateRectangleROI : NisAPIBase
    {
        private enum eQueryIndex
        {
            centerX,
            centerY,
            width,
            height,
            angleRot,
            colorRGB,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.centerX,new IntValue());
            ParamValues.Add(eQueryIndex.centerY, new IntValue());
            ParamValues.Add(eQueryIndex.width, new IntValue());
            ParamValues.Add(eQueryIndex.height, new IntValue());
            ParamValues.Add(eQueryIndex.angleRot, new DoubleValue());
            ParamValues.Add(eQueryIndex.colorRGB, new IntValue());
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.ROI.CreateRectangleROI macro
                    = new NisMacro.Net.Macro.Macros.ROI.CreateRectangleROI
                    {
                        AngleRot = ParamValues[eQueryIndex.angleRot].ToDouble,
                        ColorRGB = ParamValues[eQueryIndex.colorRGB].ToInt,
                        CenterX = ParamValues[eQueryIndex.centerX].ToInt,
                        CenterY = ParamValues[eQueryIndex.centerY].ToInt,
                        Height = ParamValues[eQueryIndex.height].ToInt,
                        Width = ParamValues[eQueryIndex.width].ToInt

                    };


                //CreateRectangleROI
                AddMacroToQueSync(macro);

                HttpResponse.ResponseParam.Add(macro.Result);
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
