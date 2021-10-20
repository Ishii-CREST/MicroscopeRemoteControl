using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HttpConnect
{
    public class RequestRelated
    {
        private static  RequestRelated myInstance = new RequestRelated();
        public static RequestRelated Instance { get { return myInstance; } }
        private RequestRelated() { }
        /// <summary>
        /// 受信用URL
        /// </summary>
        public string Port { get; set; }
        /// <summary>
        /// APIログ出力パス
        /// </summary>
        public string OutnPutAPILogPath { get; set; }
        /// <summary>
        /// APPログ出力パス
        /// </summary>
        public string OutnPutAppLogPath { get; set; }
        /// <summary>
        /// 画像ファイル格納用ディレクトリパス
        /// </summary>
        public string ImageDir { get; set; }
        /// <summary>
        /// WWWサーバーのルートフォルダ
        /// </summary>
        public string WebServerRoot { get; set; }
        /// <summary>
        /// 最大Appログ数
        /// </summary>
        public int MaxAppLogNum { get; set; }
        public void SetRelated()
        {
            this.Port = Properties.Settings.Default.PORT;
            this.OutnPutAPILogPath = Properties.Settings.Default.API_LOG;
            this.OutnPutAppLogPath = Properties.Settings.Default.APP_LOG;
            this.ImageDir = Properties.Settings.Default.IMAGE_SAVE_PATH;
            WebServerRoot = Properties.Settings.Default.WEB_SERVER_ROOT;
            this.MaxAppLogNum = int.TryParse(Properties.Settings.Default.APP_LOG_MAX_COUNT, out int count) ? 100: count;
            
        }

    }
}
