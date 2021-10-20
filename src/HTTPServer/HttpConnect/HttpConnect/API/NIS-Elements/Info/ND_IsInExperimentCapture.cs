using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class ND_IsInExperimentCapture : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                int isRunning = InternalData.NIS_Elements.NisInternalData.Instance.IsRunningNDExpriment ||
                                InternalData.NIS_Elements.NisInternalData.Instance.IsRunningNDStimulation ? 1 : 0;
                // HTTPレスポンスデータ作成
                HttpResponse.ResponseParam.Add(isRunning);
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
