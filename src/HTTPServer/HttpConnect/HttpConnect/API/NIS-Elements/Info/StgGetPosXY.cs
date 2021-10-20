using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class StgGetPosXY : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.Device.XY.StgGetPosXY macro
                    = new NisMacro.Net.Macro.Macros.Device.XY.StgGetPosXY();

                //StgGetPosXY実行
                AddMacroToQueSync(macro);

                //取得したステータスを保存
                HttpResponse.ResponseParam.Add(macro.LpStgX);
                HttpResponse.ResponseParam.Add(macro.LpStgY);
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
