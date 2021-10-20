using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.HttpUtil.Data
{
    public class ReceivedRequestData
    {
        /// <summary>
        /// リクエスト送信元クライアントPCのIPアドレス
        /// </summary>
        public string ClientIPAddress { get; set; }

        /// <summary>
        /// urlから取得した実行対象API名
        /// </summary>
        public string APIName { get; set; }

        /// <summary>
        /// HTTPリクエストから取り出したクエリストリング
        /// </summary>
        public string QueryString { get; set; }
        /// <summary>
        /// リクエストデータを解析する。
        /// </summary>
        /// <param name="request"></param>
        public void SetQuery(HttpListenerRequest request)
        {
            //API名を取得
            APIName = request.RawUrl.ToString().Replace("/", "");
            ClientIPAddress = request.RemoteEndPoint.ToString();

            StreamReader reader = new StreamReader(request.InputStream);
            QueryString = reader.ReadToEnd();
        }
        /// <summary>
        /// リクエストパラメータを取得する
        /// </summary>
        /// <returns></returns>
        public List<string> GetQueryArgList()
        {
            //操作対象チャンネルと変更要素が結合した状態で送信されるので
            //対応する変数へ格納する
            string[] paramList = QueryString.Split('&');
            return paramList.ToList();
        }
    }
}
