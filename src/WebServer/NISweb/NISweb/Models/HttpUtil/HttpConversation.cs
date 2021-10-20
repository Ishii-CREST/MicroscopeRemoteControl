using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace NISweb.Models.HttpUtil
{
    public class HttpConversation
    {
        public HttpWebResponse MessageSender(string URL, int TimeOut, string APIName = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(URL + "/" + APIName));

            //対象のAPI名の設定が無い場合、POST送信が出来ないため
            //GET送信を行い、接続確認に止める
            if (!string.IsNullOrEmpty(APIName))
            {
                request.Timeout = TimeOut;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = 0;
            }
            else
            {
                request.Method = "GET";
            }
            
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }
    }
}