using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class StgMoveXY : NisAPIBase
    {

        private enum eQueryIndex
        {
            stageX,
            stageY,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.stageX, new DoubleValue());
            ParamValues.Add(eQueryIndex.stageY, new DoubleValue());
            CreateParamList(typeof(eQueryIndex));
        }



        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.Device.XY.StgMoveXY macro
                    = new NisMacro.Net.Macro.Macros.Device.XY.StgMoveXY()
                    {
                        stgX = ParamValues[eQueryIndex.stageX].ToDouble,
                        stgY = ParamValues[eQueryIndex.stageY].ToDouble
                    };

                //StgMoveXYの実行
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
