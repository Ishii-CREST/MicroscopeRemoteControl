using HttpConnect.InternalData.NIS_Elements;
using NisMacro.Net.Execute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class GetCurrentNDExpXYZ : NisAPIBase
    {
        #region method
        protected override bool RunAPIImpl()
        {
            // ※他マクロ実行中に実行されることが予想されるため、実行するマクロはマクロキューに追加せずに実施。
            try
            {
                // 実行していない場合は全て-1
                if (NisInternalData.Instance.IsRunningNDExpriment)
                {

                    bool enableT = NisInternalData.Instance.LastNDExperimentInfo.enableT;
                    bool enableXY = NisInternalData.Instance.LastNDExperimentInfo.enableXY;
                    bool enableZ = NisInternalData.Instance.LastNDExperimentInfo.enableZ;
                    int countXYmax = NisInternalData.Instance.LastNDExperimentInfo.XYCount;
                    int countTmax = NisInternalData.Instance.LastNDExperimentInfo.loopT.Sum();
                    int countZmax = NisInternalData.Instance.LastNDExperimentInfo.ZStepCount;

                    // ファイル名からt,xy,zを取得して、次の画像番号を取得。
                    // 最新ディレクトリを取得
                    var newestDir = UserUtil.UtilMethods.GetNewestImageDirectory(RequestRelated.Instance.ImageDir);
                    if (string.IsNullOrEmpty(newestDir))
                    {
                        // 最新が見つからない。
                        HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                        OutputLog("There is no Image Directory");
                        return false;
                    }

                    // ファイルの数を取得→取得済み枚数
                    var fileList = new DirectoryInfo(newestDir).GetFiles("*.tif", SearchOption.AllDirectories);
                    // 最新の画像のTXYZを取得
                    GetNewestTXYZ(fileList, out int maxT, out int maxXY, out int maxZ);

                    ////////////
                    // 次カウントを計算
                    int nextT = maxT;
                    int nextXY =maxXY;
                    int nextZ = maxZ;


                    if (enableZ)
                    {
                        if (maxZ == countZmax)
                        {
                            // Zが最大の場合
                            // Tが最大の場合は増やさない。
                            // XYが最大の場合は増やす。
                            if (enableT && enableXY)
                            {
                                if (maxXY != countXYmax) nextZ = 1;
                            }
                            else if (enableT && !enableXY)
                            {
                                if (maxT != countXYmax) nextZ = 1;
                            }
                            else if (!enableT && enableXY)
                            {
                                if (maxXY != countXYmax) nextZ = 1;
                            }
                            else if (!enableT && !enableXY)
                            {
                                nextZ++;
                            }

                        }
                        else if(maxZ == -1)
                        {
                            nextZ = 1;
                        }
                        else
                        {
                            // Zが最大ではない場合次のカウンタへ
                            nextZ++;
                        }
                    }
                    else
                    {
                        nextZ = -1;
                    }
                    if (enableXY)
                    {
                        if (maxXY == countXYmax)
                        {
                            if (enableT && enableZ)
                            {
                                if (maxT != countTmax && maxZ == countZmax) nextXY = 1;
                            }
                            else if (enableT && !enableZ)
                            {
                                if (maxT != countTmax) nextXY = 1;
                            }
                            else if (!enableT && enableZ)
                            {
                                //
                            }
                        }
                        else if(maxXY == -1)
                        {
                            nextXY = 1;
                        }
                        else
                        {
                            if (enableT && enableZ)
                            {
                                if (maxZ == countZmax) nextXY++;
                            }
                            else if (enableT && !enableZ)
                            {
                                nextXY++;
                            }
                            else if (!enableT && enableZ)
                            {
                                if (maxZ == countZmax) nextXY++;
                            }
                            else if (!enableT && !enableZ)
                            {
                                nextXY++;
                            }
                        }
                    }
                    else
                    {
                        nextXY = -1;
                    }

                    if (enableT)
                    {
                        if (maxT == -1)
                        {
                            nextT = 1;
                        }
                        else
                        {
                            if (enableXY && enableZ)
                            {
                                if (maxT != countTmax &&
                                    maxXY == countXYmax &&
                                    maxZ == countZmax) nextT++;
                            }
                            else if (enableXY && !enableZ)
                            {
                                if (maxT != countTmax &&
                                    maxXY == countXYmax) nextT++;
                            }
                            else if (!enableXY && enableZ)
                            {
                                if (maxT != countTmax &&
                                    maxZ == countZmax) nextT++;
                            }
                            else if (!enableXY && !enableZ)
                            {
                                nextT++;
                            }
                        }
                    }
                    else
                    {
                        nextT = -1;
                    }
                    HttpResponse.ResponseParam.Add(nextT);
                    HttpResponse.ResponseParam.Add(nextXY);
                    HttpResponse.ResponseParam.Add(nextZ);
                }
                else
                {
                    HttpResponse.ResponseParam.Add(-1);
                    HttpResponse.ResponseParam.Add(-1);
                    HttpResponse.ResponseParam.Add(-1);
                }
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                OutputLog("", e);
                return false;
            }
        }

        /// <summary>
        /// ファイル名のフォーマット(正規表現)を取得する。
        /// </summary>
        /// <param name="t"></param>
        /// <param name="xy"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        private string GetFilePrefixRegFormat(bool t, bool xy, bool z)
        {
            List<string> retList = new List<string>();
            string digitStr = "[0-9]";
            if (t) retList.Add(CommonDefine.FILE_T_PEFIX + digitStr + "*");
            if (xy) retList.Add(CommonDefine.FILE_XY_PEFIX + digitStr + "*");
            if (z) retList.Add(CommonDefine.FILE_Z_PEFIX + digitStr + "*");
            retList.Add(CommonDefine.FILE_CH_PEFIX + digitStr + "*");
            return string.Join("", retList.ToArray()) + ".tif";
        }

        private string GetFilePrefix(int t, int xy, int z)
        {
            List<string> retList = new List<string>();
            string digitStr = "[0-9]";
            if (t > 0) retList.Add(CommonDefine.FILE_T_PEFIX + t.ToString() + "*");
            if (xy > 0) retList.Add(CommonDefine.FILE_XY_PEFIX + xy.ToString() + "*");
            if (z > 0) retList.Add(CommonDefine.FILE_Z_PEFIX + z.ToString() + "*");
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
        #endregion


    }
}
