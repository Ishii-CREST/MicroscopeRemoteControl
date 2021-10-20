using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using System.IO;
using NISweb.Controllers;
using NISweb.Models.HttpUtil;

namespace NISweb.Models.HttpUtil
{
    public class HttpMessageController
    {
        private string URL = "http://{0}:{1}";
        
        /// <summary>
        /// URL作成
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="Port"></param>
        public string CreateURL(string IP, string Port)
        {
            return URL = string.Format(URL, IP, Port);
        }
        
        /// <summary>
        /// ポーリング
        /// </summary>
        /// <returns></returns>
        public HttpWebResponse Polling(string IP, string Port, int TimeOut, string APIName = null)
        {
            HttpWebResponse response = null;
            try
            {
                HttpConversation httpConversation = new HttpConversation();
                response = httpConversation.MessageSender(CreateURL(IP, Port), TimeOut, APIName);
            }
            catch (Exception e) 
            {
                var errorMsg = e.Message.ToString();
                response = null;
            }
            return response;
        }

        /// <summary>
        /// HttpWebResponseをstringに変換
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public bool ResponseToString(HttpWebResponse response, ref string strParam)
        {
            bool statusFlg = false;
            //リクエストが不正だとレスポンスがnull
            if (response == null) return statusFlg;

            try
            {
                using (response)
                {
                    if (response != null)
                    {
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                        {
                            strParam = reader.ReadToEnd();
                        }
                    }
                    statusFlg = response.StatusCode.ToString() == "OK" ? true : false;
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }

            return statusFlg;
        }
    }
}
