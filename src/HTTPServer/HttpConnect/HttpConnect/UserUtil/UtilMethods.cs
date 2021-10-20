using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.UserUtil
{
    public static class UtilMethods
    {
        /// <summary>
        /// バージョン文字列を取得します。
        /// </summary>
        /// <returns></returns>
        public static string GetVersionStr()
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            //バージョンの取得
            System.Version ver = asm.GetName().Version;

            return "version " + ver.ToString();
        }

        /// <summary>
        /// 文字列がnullの場合は空文字、そうでない場合は文字自体を返します。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetEmptyOrString(string str)
        {
            return string.IsNullOrEmpty(str)? string.Empty : str;
        }

        /// <summary>
        /// 画像保存フォルダで一番新しいフォルダを取得します
        /// </summary>
        /// <param name="imageDir">最新のフォルダ。見つからない場合はnull</param>
        /// <returns></returns>
        public static string GetNewestImageDirectory(string imageDir)
        {
            // 更新日付ではなく、yyyymmdssで最も新しい日付
            if (!Directory.Exists(imageDir)) return null;
            var dirList = Directory.GetDirectories(imageDir);
            DateTime newestDateTime = DateTime.MinValue;
            string newestDir = string.Empty;
            foreach(var dir in dirList)
            {
                //// yyyyMMdd_HHmmssのフォルダを列挙する。

                // 日付フォーマットにマッチするか調べる。
                //if(reg.IsMatch(dir))
                //{
                    // 日付部と時間部に分ける
                    //DateTimeに変換
                    // ※GetFileNameしているが、Directory名が取得できる
                    if(DateTime.TryParseExact(Path.GetFileName(dir), 
                        CommonDefine.DATETIME_FORMAT, 
                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                        System.Globalization.DateTimeStyles.None, out DateTime date))
                    {
                        if (newestDateTime < date)
                        {
                            newestDateTime = date;
                            newestDir = dir;
                        }
                    }
                //}
            }
            return newestDir;
        }

        public static IPAddress[] GetMyIP()
        {
            // ホスト名を取得する
            string hostname = Dns.GetHostName();

            // ホスト名からIPアドレスを取得する
            return  Dns.GetHostAddresses(hostname);
        }

    }
}
