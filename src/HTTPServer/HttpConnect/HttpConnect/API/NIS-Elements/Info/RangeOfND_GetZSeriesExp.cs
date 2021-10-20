using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    /// <summary>
    /// 4D撮影時のZ幅取得
    /// </summary>
    public class RangeOfND_GetZSeriesExp : NisAPIBase
    {

        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.ND.ZSeries.ND_GetZSeriesExp macro
                    = new NisMacro.Net.Macro.Macros.ND.ZSeries.ND_GetZSeriesExp();

                AddMacroToQueSync(macro);

                //Range
                double range = System.Math.Abs(macro.ND_LpZSeriesTop - macro.ND_LpZSeriesBottom);

                // HTTPレスポンスデータ作成
                HttpResponse.ResponseParam.Add(range);
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
