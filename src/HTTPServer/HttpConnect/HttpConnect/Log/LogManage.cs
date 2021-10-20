using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace HttpConnect
{
    /// <summary>
    ///　ロギングクラス
    /// </summary>
    public class LogManage
    {
        private const string DATETIME_FORMAT = "yyyyMMdd";
        public LogManage(string logDir)
        {
            LogSaveDir = logDir;

            // 保存場所が無ければ作る。
            if (!Directory.Exists(LogSaveDir))
            {
                // 作成
                Directory.CreateDirectory(LogSaveDir);
            }

        }

        public string LogSaveDir { get; private set; }

        private object lockObj = new object();
        /// <summary>
        /// Logファイル名
        /// </summary>
        private string LogName
        {
            get
            {
                return string.Format(@"{0}\{1}.log", LogSaveDir, DateTime.Now.ToString(DATETIME_FORMAT));
            }
        }
        /// <summary>
        /// Log出力
        /// </summary>
        /// <param name="message"></param>
        public void OutputLog(string clientIP, string apiName, string message)
        {
            // 保存場所が無ければ作る。
            OutputLog(CreateLogContent(clientIP, apiName, message));
        }

        /// <summary>
        /// Log出力
        /// </summary>
        /// <param name="message"></param>
        public void OutputLog(string message)
        {
            try
            {
                // 保存場所が無ければ作る。
                if (!Directory.Exists(Path.GetDirectoryName(LogSaveDir)))
                {
                    // 作成
                    Directory.CreateDirectory(Path.GetDirectoryName(LogSaveDir));
                }

                lock (lockObj)
                {
                    Trace.WriteLine(CreateLogContent(message));
                    using (FileStream fs = new FileStream(LogName,
                              FileMode.Append, FileAccess.Write, FileShare.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(
                            fs,
                            Encoding.GetEncoding("utf-8")))
                        {
                            writer.WriteLine((CreateLogContent(message)));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// エラーログ出力を行う。
        /// </summary>
        /// <param name="message"></param>
        public void OutputLog(string message, Exception ex)
        {
            OutputLog(string.Format("{0}" + System.Environment.NewLine + "{1}{2}", message, ex.Message, ex.StackTrace));
        }

        /// <summary>
        /// 過去Logファイル削除
        /// </summary>
        public void DeleteLog()
        {
            if (Directory.Exists(LogSaveDir))
            {
                string[] files = Directory.GetFiles(LogSaveDir);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLog"></param>
        public void DeleteLog(int maxLog)
        {
            if (Directory.Exists(LogSaveDir))
            {
                string[] files = Directory.GetFiles(LogSaveDir).OrderBy(d => DateTime.ParseExact(Path.GetFileNameWithoutExtension(d), DATETIME_FORMAT, null)).ToArray();
                files = files.Take(Math.Min(files.Length, maxLog)).ToArray();
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// Log内容作成
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string CreateLogContent(string clientIP, string apiName, string message)
        {
            StringBuilder logContent = new StringBuilder();
            logContent.Append(clientIP);
            logContent.Append(",");
            logContent.Append(apiName);
            logContent.Append(",");
            logContent.Append(message);
            return logContent.ToString();
        }
        /// <summary>
        /// Log内容作成
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string CreateLogContent(string message)
        {
            StringBuilder logContent = new StringBuilder();
            logContent.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            logContent.Append(",");
            logContent.Append(message);
            return logContent.ToString();
        }
    }
}
