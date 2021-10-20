using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NISweb.Common;

namespace NISweb.Models.FileUtil
{
    public class GetLatestImage
    {
        //最新ディレクトリを取得
        private string GetNewestImageDirectory(string imageDir)
        {
            // 更新日付ではなく、yyyymmdssで最も新しい日付
            if (!Directory.Exists(imageDir)) return null;
            var dirList = Directory.GetDirectories(imageDir);
            DateTime newestDateTime = DateTime.MinValue;
            string newestDir = string.Empty;
            foreach (var dir in dirList)
            {
                //DateTimeに変換
                // ※GetFileNameしているが、Directory名が取得できる
                if (DateTime.TryParseExact(Path.GetFileName(dir),
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
            }
            return newestDir;
        }

        private string GetFilePrefix(int t, int xy, int z)
        {
            List<string> retList = new List<string>();
            string digitStr = "[0-9]";
            if (xy > 0) retList.Add(CommonDefine.FILE_XY_PEFIX + xy.ToString() + "*");
            if (z > 0) retList.Add(CommonDefine.FILE_Z_PEFIX + z.ToString() + "*");
            if (t > 0) retList.Add(CommonDefine.FILE_T_PEFIX + t.ToString() + "*");
            retList.Add(CommonDefine.FILE_CH_PEFIX + digitStr + "*");
            return string.Join("", retList.ToArray()) + ".tif";
        }

        /// <summary>
        /// 最新の画像郡(t,xy,zが最新のch画像セット)を取得する
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private List<FileInfo> GetNewestFiles(FileInfo[] files)
        {
            // 一番新しいファイルを取得
            List<FileInfo> newestFileInfo;
            int maxT = -1;
            int maxXY = -1;
            int maxZ = -1;

            GetNewestTXYZ(files, out maxT, out maxXY, out maxZ);

            newestFileInfo = files.ToList().Where(d =>
            {
                Regex regT = new Regex(GetFilePrefix(maxT, maxXY, maxZ),
                 RegexOptions.IgnoreCase | RegexOptions.Singleline);
                return regT.IsMatch(d.Name);
            }).ToList();

            return newestFileInfo;
        }
        
        private void GetNewestTXYZ(FileInfo[] fileList, out int t, out int xy, out int z)
        {
            // 一番新しいファイルを取得
            t = -1;
            xy = -1;
            z = -1;

            foreach (var f in fileList)
            {
                int tempT, tempXY, tempZ, tempC;
                GetTXYZFromFileName(f.Name, out tempT, out tempXY, out tempZ, out tempC);

                if (t < tempT)
                {
                    t = tempT;
                    xy = -1;
                    z = -1;
                }
                if (xy < tempXY)
                {
                    xy = tempXY;
                    z = -1;
                }
                if (z < tempZ)
                {   
                    z = tempZ;
                }
            }
        }

        /// <summary>
        /// ファイル名からT,XY,Z,Cのカウンタを取得する。
        /// </summary>
        /// <param name="fileName">ファイル名(ファイル名のみ)</param>
        /// <param name="t">T</param>
        /// <param name="xy">XY</param>
        /// <param name="z">Z</param>
        /// <param name="c">Channel</param>
        private void GetTXYZFromFileName(string fileName, out int t, out int xy, out int z, out int c)
        {
            t = -1;
            xy = -1;
            z = -1;
            c = -1;

            Regex regT = new Regex("" + CommonDefine.FILE_T_PEFIX + "(?<T>[0-9]+)",
                 RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Regex regXY = new Regex("" + CommonDefine.FILE_XY_PEFIX + "(?<XY>[0-9]+)",
                 RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Regex regZ = new Regex("" + CommonDefine.FILE_Z_PEFIX + "(?<Z>[0-9]+)",
                 RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Regex regCh = new Regex("" + CommonDefine.FILE_CH_PEFIX + "(?<Ch>[0-9]+)",
            RegexOptions.IgnoreCase | RegexOptions.Singleline);

            int.TryParse(regT.Match(fileName).Groups[1].Value, out t);
            int.TryParse(regXY.Match(fileName).Groups[1].Value, out xy);
            int.TryParse(regZ.Match(fileName).Groups[1].Value, out z);
            int.TryParse(regCh.Match(fileName).Groups[1].Value, out c);
        }

        //最新ファイルを取得
        public bool GetGetCurrentImage(string localImageDir, string serverImageDir, ref string DebugStr)
        {
            var newestDir = GetNewestImageDirectory(localImageDir);

            if (string.IsNullOrEmpty(newestDir))
            {
                // 最新が見つからない。
                return false;
            }

            DirectoryInfo newestDirInfo = new DirectoryInfo(newestDir);

            // 最新の画像(TIF)をコピー。
            var XYZTfileList = GetNewestFiles(newestDirInfo.GetFiles("*.tif", SearchOption.AllDirectories));

            try
            {
                //コピー先Dir
                DirectoryInfo serverDir = new DirectoryInfo(serverImageDir);

                foreach (var f in XYZTfileList)
                {
                    // コピー
                    f.CopyTo(Path.Combine(serverDir.FullName, f.Name), false);
                }
                //XY,Z,Tが最新じゃない時
                if (0 == XYZTfileList.Count())
                {
                    var fileList = newestDirInfo.GetFiles("*.tif", SearchOption.AllDirectories);
                    foreach (var f in fileList)
                    {
                        // コピー
                        f.CopyTo(Path.Combine(serverDir.FullName, f.Name), false);
                    }
                }
            }
            catch (Exception e)
            {
                DebugStr = e.Message.ToString();
            }
            return true;
        }

    }
}