using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HttpConnect
{
    public static class LogManage
    {
        /// <summary>
        /// Logファイル名
        /// </summary>
        private static readonly string LogName
            = string.Format(@"{0}\{1}.log", RequestRelated.OutnPutLogPath, DateTime.Now.ToString("yyyyMMdd"));

        /// <summary>
        /// Log出力
        /// </summary>
        /// <param name="message"></param>
        public static void OutputLog(string key, string message)
        {
            // 保存場所が無ければ作る。
            if(!Directory.Exists(Path.GetDirectoryName(LogName)))
            {
                // 作成
                Directory.CreateDirectory(Path.GetDirectoryName(LogName));
            }

            using (StreamWriter writer = new StreamWriter(
                LogName,
                true,
                Encoding.GetEncoding("utf-8")))
            {
                writer.Write(CreateLogContent(key, message));
                writer.Write("\r\n");
            }
        }

        /// <summary>
        /// 過去Logファイル削除
        /// </summary>
        public static void DeleteLog()
        {
            string[] files = Directory.GetFiles(RequestRelated.OutnPutLogPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }

        /// <summary>
        /// Log内容作成
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static string CreateLogContent(string key, string message)
        {
            StringBuilder logContent = new StringBuilder();
            logContent.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            logContent.Append(",");
            logContent.Append(APIElements.ClientIPAddress);
            logContent.Append(",");
            logContent.Append(key);
            logContent.Append(",");
            logContent.Append(message);
            return logContent.ToString();
        }
    }
}
