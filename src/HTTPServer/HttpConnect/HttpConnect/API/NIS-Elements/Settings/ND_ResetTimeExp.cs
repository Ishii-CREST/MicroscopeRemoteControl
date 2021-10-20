using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    /// <summary>
    /// The last used settings of the experiment are stored 
    /// in memory. This function removes the settings of 
    /// the current Z-series experiment.
    /// </summary>
    public class ND_ResetTimeExp : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_ResetTimeExp macro =
                    new NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_ResetTimeExp();

                //ND_ResetZSeriesExp
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
