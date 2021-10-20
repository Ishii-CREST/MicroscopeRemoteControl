using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    /// <summary>
    /// ND撮影の総撮像回数
    /// </summary>
    public class GetNDExperimentImageCount : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                // 実行中でなかったら-1
                if (InternalData.NIS_Elements.NisInternalData.Instance.IsRunningNDExpriment)
                {
                    ////全要素の積
                    int progress = InternalData.NIS_Elements.NisInternalData.Instance.LastNDExperimentInfo.TotalImagingCount;

                    // HTTPレスポンスデータ作成
                    HttpResponse.ResponseParam.Add(progress);
                }
                else
                {
                    HttpResponse.ResponseParam.Add(-1);
                }
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
