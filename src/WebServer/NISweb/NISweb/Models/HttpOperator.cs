using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Configuration;
using System.IO;

namespace NISweb.Models
{
    public static class HttpConnectSet
    {
        /// <summary>
        /// サーバーIPアドレス
        /// </summary>
        public static string ServerIP { get; set; }
        /// <summary>
        /// サーバーポート
        /// </summary>
        public static string ServerPort { get; set; }
        /// <summary>
        /// HTTPリクエストのタイムアウト
        /// </summary>
        public static int TimeOut { get; set; }
        /// <summary>
        /// サーバ接続ステータス
        /// </summary>
        public static string ConnectionStatus { get; set; }
        /// <summary>
        /// ブラウザアクセス時の日時(最新画像のタイムスタンプとの比較に利用)
        /// </summary>
        public static DateTime AccessTime { get; set; }
    }

    public static class HttpRequest
    {
        static string URL = "http://{0}:{1}";

        public static void CreateURL(string IP, string Port)
        {
            URL = string.Format(URL, IP, Port);
        }

        /// <summary>
        /// Web.configに設定したサーバIP・Portを読み取り
        /// </summary>
        public static void ReadConf()
        {
            HttpConnectSet.ServerIP = ConfigurationManager.AppSettings["IP"];
            HttpConnectSet.ServerPort = ConfigurationManager.AppSettings["Port"];
            HttpConnectSet.TimeOut = int.Parse(ConfigurationManager.AppSettings["RequestTimeOut"]);
            HttpConnectSet.ConnectionStatus = "Disconnected";
            HttpConnectSet.AccessTime = DateTime.Now;

            FileCollection.LocalImagePath = ConfigurationManager.AppSettings["LocalImagePath"];
            FileCollection.ServerImagePath = ConfigurationManager.AppSettings["ServerImagePath"];
            FileCollection.ServerLogPath = ConfigurationManager.AppSettings["ServerLogPath"];
            FileCollection.ServerContentPath = ConfigurationManager.AppSettings["ContentDir"];
            FileCollection.LogPath = ConfigurationManager.AppSettings["LogPath"];
            FileCollection.DeleteLog();

            CreateURL(HttpConnectSet.ServerIP, HttpConnectSet.ServerPort);
        }

        /// <summary>
        /// ポーリング
        /// </summary>
        /// <returns></returns>
        public static void Polling()
        {
            try
            {
                HttpWebResponse response = HttpConversation.MessageSender(URL);

                if (response.StatusCode.ToString() == "OK")
                {
                    HttpConnectSet.ConnectionStatus = "Connected";
                }
            }
            catch (Exception e)
            {
                HttpConnectSet.ConnectionStatus = "Disconnected";
            }
        }
    }
}
