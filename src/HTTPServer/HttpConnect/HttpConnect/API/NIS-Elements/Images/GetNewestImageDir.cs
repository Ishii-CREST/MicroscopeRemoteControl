using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Images
{
    public class GetNewestImageDir : NisAPIBase
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

                ///////////////////////
                // 見つかったらWebページ(公開用)のに画像転送用添付フォルダを作成する。(ない場合)
                DirectoryInfo imageTempDir  =new DirectoryInfo(Path.Combine(RequestRelated.Instance.WebServerRoot, CommonDefine.TRANS_IMAGE_TEMP_DIRNAME));
                if (!imageTempDir.Exists)
                {
                    imageTempDir.Create();
                }
                else
                {
                    // あった場合は一度すべて消して、作り直し。
                    imageTempDir.Delete(true);
                    imageTempDir.Create();
                }

                ///////////////////////////
                //　公開できるように公開許可を書いたweb.configをコピーする。
                string webConfigPath = Path.Combine(imageTempDir.FullName, CommonDefine.WEB_CONFIG);
                if (File.Exists(InternalData.NIS_Elements.NisInternalData.Instance.WebConfigPath))
                {
                    File.Copy(InternalData.NIS_Elements.NisInternalData.Instance.WebConfigPath, webConfigPath, true);
                }
                else
                {
                    OutputLog(string.Format("Web.config is not found. file[{0}]", InternalData.NIS_Elements.NisInternalData.Instance.WebConfigPath));
                }

                ////////////////////////
                // 最新の画像(TIF)をコピー。
                var fileList = newestDirInfo.GetFiles("*.tif", SearchOption.AllDirectories);
                string baseURL = string.Format("http://{0}/{1}/",
                    UserUtil.UtilMethods.GetMyIP().Where(d => d.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ElementAt(0).ToString(),
                    CommonDefine.TRANS_IMAGE_TEMP_DIRNAME);
                foreach (var f in fileList)
                {
                    // コピー
                    f.CopyTo(Path.Combine(imageTempDir.FullName, f.Name), false);
                }

                HttpResponse.ResponseParam.Add(baseURL);         // 公開用画像ディレクトリURLを返す。
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
