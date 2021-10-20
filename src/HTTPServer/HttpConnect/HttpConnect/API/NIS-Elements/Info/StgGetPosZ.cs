using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class StgGetPosZ : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.Device.Z.StgGetPosZ macro
                    = new NisMacro.Net.Macro.Macros.Device.Z.StgGetPosZ();

                //StgGetPosZ実行
                AddMacroToQueSync(macro);

                HttpResponse.ResponseParam.Add(macro.LpStgZ);
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
