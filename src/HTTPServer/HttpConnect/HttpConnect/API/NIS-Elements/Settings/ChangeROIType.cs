using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class ChangeROIType : NisAPIBase
    {
        private enum eQueryIndex
        {
            ID = 0,
            ROIType,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.ID,
                new IntValue()
                {
                    MinValue = 0,
                });
            ParamValues.Add(eQueryIndex.ROIType,
                new IntValue()
                {
                    // 今後変化する可能性を考慮しあえて範囲を設けない
                });
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            
            try
            {
                
                NisMacro.Net.Macro.Macros.ROI.ChangeROIType macro
                    = new NisMacro.Net.Macro.Macros.ROI.ChangeROIType()
                {
                     RoiId = ParamValues[eQueryIndex.ID].ToInt,
                     RoiType = ParamValues[eQueryIndex.ROIType].ToInt
                    };
                //ChangeROIType
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
