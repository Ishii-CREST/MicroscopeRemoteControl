using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using NISweb.Common;
using System.Text.RegularExpressions;

namespace NISweb.Models.FileUtil
{
    public class LogHelper
    {
        private DataTable logTable;
        
        public LogHelper()
        {
            logTable = new DataTable();
            logTable.Columns.Add("Run Time");
            logTable.Columns.Add("Client");
            logTable.Columns.Add("Instruction");
            logTable.Columns.Add("Status");
        }

        /// <summary>
        /// Logの中身取得
        /// </summary>
        public void GetLogFile(string LogPath, string ServerLogPath,
            ref List<string> RunTime,
            ref List<string> Client,
            ref List<string> Instruction,
            ref List<string> Status,
            ref string DebugStr)
        {
            try
            {
                //コピー先Dir
                DirectoryInfo serverDir = new DirectoryInfo(ServerLogPath);

                //ファイル名を取得
                var file = GetNewestLogDirectory(LogPath);
                if (!string.IsNullOrEmpty(file))
                {
                    //Logファイルコピー
                    FileInfo fileInfo = new FileInfo(file);

                    //コピー時ファイル名作成
                    string logFileName = Path.Combine(serverDir.FullName, fileInfo.Name);

                    // コピー
                    fileInfo.CopyTo(logFileName, true);

                    //Logファイル全読込
                    string textAllLine = File.ReadAllText(logFileName, System.Text.Encoding.UTF8);
                    //行に分割
                    string[] textArray = textAllLine.Replace(@"\r\n", @"\n").Split('\n');

                    textArray.ToList().ForEach(x =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(x))
                            {
                                logTable.Rows.Add(Regex.Split(x, ","));
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    });

                    for (int i = 0; i < logTable.Rows.Count; i++)
                    {
                        RunTime.Add(logTable.Rows[i]["Run Time"].ToString());
                        Client.Add(logTable.Rows[i]["Client"].ToString());
                        Instruction.Add(logTable.Rows[i]["Instruction"].ToString());
                        Status.Add(logTable.Rows[i]["Status"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                DebugStr = e.Message.ToString();
            }
        }

        /// <summary>
        /// 最新Logファイル取得
        /// </summary>
        /// <param name="imageDir"></param>
        /// <returns></returns>
        private string GetNewestLogDirectory(string imageDir)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(imageDir);
            // 更新日付ではなく、yyyymmdssで最も新しい日付
            var dirList = dirInfo.GetFiles("*.log", SearchOption.AllDirectories);
            DateTime newestDateTime = DateTime.MinValue;
            string newestDir = string.Empty;
            foreach (var dir in dirList)
            {
                //DateTimeに変換
                // ※GetFileNameしているが、Directory名が取得できる
                if (DateTime.TryParseExact(dir.ToString().Replace(".log", string.Empty),
                    CommonDefine.DATE_FORMAT,
                    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                    System.Globalization.DateTimeStyles.None, out DateTime date))
                {
                    if (newestDateTime < date)
                    {
                        newestDateTime = date;
                        newestDir = string.Format(@"{0}\{1}", imageDir, dir.ToString());
                    }
                }
            }
            return newestDir;
        }
    }
}