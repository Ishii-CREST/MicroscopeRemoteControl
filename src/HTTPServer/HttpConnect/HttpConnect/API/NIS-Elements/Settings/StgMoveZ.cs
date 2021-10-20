using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class StgMoveZ : NisAPIBase
    {
        private enum eQueryIndex
        {
            stageZ,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.stageZ, new DoubleValue());
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.Device.Z.StgMoveZ macro
                    = new NisMacro.Net.Macro.Macros.Device.Z.StgMoveZ()
                    {
                        stgZ = ParamValues[eQueryIndex.stageZ].ToDouble
                    };

                //StgMoveZの実行
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
