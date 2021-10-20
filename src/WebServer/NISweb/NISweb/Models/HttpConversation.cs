using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace NISweb.Models
{
    public static class MicroscopeStatus
    {
        /// <summary>
        /// 顕微鏡名称
        /// </summary>
        public static string MicroscopeName { get; set; }
        /// <summary>
        /// ステージ座標(X:Y:Z)
        /// </summary>
        public static string[] Stage { get; set; }
    }

    public static class HttpConversation
    {
        public static HttpWebResponse MessageSender(string URL)
        {
            //1/15 HTTPサーバテスト用 
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL + "/?Capture"));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL));

            request.Timeout = HttpConnectSet.TimeOut;
            request.Method = "GET";
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //レスポンス内容取得(仮)
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
            {
                string message = reader.ReadToEnd();
            }

            return response;
        }
    }
}