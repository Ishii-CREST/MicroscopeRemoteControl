using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Images
{
    public class GetNewestImageDateTime : NisAPIBase
    {
		protected override bool RunAPIImpl()
        {
            try
            {
                // 最新ディレクトリを取得
                var newestDir = UserUtil.UtilMethods.GetNewestImageDirectory(RequestRelated.Instance.ImageDir);

                if (string.IsNullOrEmpty(newestDir))
                {
                    // 最新が見つからない。
                    HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                    return false;
                }

                DirectoryInfo newestDirInfo = new DirectoryInfo(newestDir);

                HttpResponse.ResponseParam.Add(newestDirInfo.Name);        // フォルダ名(日付_時間)を返す。
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;
                return true;

            }
            catch (Exception ex)
            {
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                OutputLog("", ex);
                return false;
            }
        }

    }
}
