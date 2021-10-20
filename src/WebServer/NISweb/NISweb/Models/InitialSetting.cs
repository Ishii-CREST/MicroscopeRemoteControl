using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace NISweb.Models
{
    public class InitialSetting
    {
        /// <summary>
        /// サーバーIPアドレス
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// サーバーポート
        /// </summary>
        public string ServerPort { get; set; }
        /// <summary>
        /// HTTPリクエストのタイムアウト
        /// </summary>
        public int TimeOut { get; set; }
        /// <summary>
        /// ローカルのImageディレクトリパス
        /// </summary>
        public string LocalImagePath { get; set; }
        /// <summary>
        /// サーバのImageディレクトリパス
        /// </summary>
        public string ServerImagePath { get; set; }
        /// <summary>
        /// サーバの最新画像フォルダパス
        /// </summary>
        public string ServerCurrentImagePath { get; set; }
        /// <summary>
        /// 画面表示用パス
        /// </summary>
        public string ServerContentPath { get; set; }
        /// <summary>
        /// 最新画面表示用パス
        /// </summary>
        public string ServerCurrentContentPath { get; set; }
        /// <summary>
        /// 監視対象Logフォルダ
        /// </summary>
        public string LogPath { get; set; }
        /// <summary>
        /// Webサーバ側のLog格納フォルダ
        /// </summary>
        public string ServerLogPath { get; set; }
        /// <summary>
        /// ND_Acquisitionフラグ
        /// </summary>
        public string RunAcquisitionFlg = string.Empty;
        /// <summary>
        /// Xステージ
        /// </summary>
        public string XYstg = ",";
        /// <summary>
        /// Yステージ
        /// </summary>
        public string Zstg = string.Empty;
        /// <summary>
        /// ファイル削除時のメッセージ
        /// </summary>
        public string DeleteMessage { get; set; }

        private static InitialSetting instance;

        public static InitialSetting GetInstance()
        {
            if (instance == null)
            {
                instance = new InitialSetting();
            }
            return instance;
        }
        
        private InitialSetting()
        {
            //設定ファイル読込
            ReadConfig();
        }
        
        /// <summary>
        /// Web.configの読込み
        /// </summary>
        private void ReadConfig()
        {
            ServerIP = ConfigurationManager.AppSettings["IP"];
            ServerPort = ConfigurationManager.AppSettings["Port"];
            TimeOut = int.Parse(ConfigurationManager.AppSettings["RequestTimeOut"]);
            LocalImagePath = ConfigurationManager.AppSettings["LocalImagePath"];
            ServerImagePath = ConfigurationManager.AppSettings["ServerImagePath"];
            ServerCurrentImagePath = ConfigurationManager.AppSettings["ServerCurrentImagePath"];
            ServerLogPath = ConfigurationManager.AppSettings["ServerLogPath"];
            ServerContentPath = ConfigurationManager.AppSettings["ContentDir"];
            ServerCurrentContentPath = ConfigurationManager.AppSettings["CurrentImageDir"];
            LogPath = ConfigurationManager.AppSettings["LogPath"];
        }

       

        /// <summary>
        /// 過去に取得したファイルを削除
        /// </summary>
        public void DeleteFiles(string folderPath)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                
                foreach (var file in files)
                {
                    System.IO.FileInfo fileInfo = new FileInfo(file);

                    fileInfo.Attributes = System.IO.FileAttributes.Normal;
                    fileInfo.Delete();
                }
            }
            catch (Exception e)
            {
                DeleteMessage = e.Message.ToString();
            }
        }
    }
}