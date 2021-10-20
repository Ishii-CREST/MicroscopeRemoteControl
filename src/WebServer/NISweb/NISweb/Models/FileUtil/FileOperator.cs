using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NISweb.Common;
using System.Data;

namespace NISweb.Models.FileUtil
{
    public class FileOperator
    {
        //----------API Log用List----------
        /// <summary>
        /// NISマクロ実行時間
        /// </summary>
        public List<string> RunTime;
        /// <summary>
        /// マクロ送信クライアントIPアドレス
        /// </summary>
        public List<string> Client;
        /// <summary>
        /// 実行対象マクロ名
        /// </summary>
        public List<string> Instruction;
        /// <summary>
        /// 実行結果
        /// </summary>
        public List<string> Status;
        
        //I----------ImageListView用プロパティ----------
        /// <summary>
        /// ファイルフルパス
        /// </summary>
        public string[] FileFullPath { get; set; }
        /// <summary>
        /// ファイル作成日時
        /// </summary>
        public string[] FileCreationDate { get; set; }
        /// <summary>
        /// ファイルサイズ
        /// </summary>
        public string[] FileLength { get; set; }
        /// <summary>
        /// ファイル名
        /// </summary>
        public string[] FileName { get; set; }
        /// <summary>
        /// ファイル格納場所
        /// </summary>
        public string[] FileLink { get; set; }
        
        private static FileOperator instance;


        public static FileOperator GetInstance()
        {
            if (instance == null)
            {
                instance = new FileOperator();
            }
            return instance;
        }
        

        /// <summary>
        /// ローカルのTIFファイル情報取得
        /// </summary>
        public void TransportFiles(string LocalImagePath)
        {
            //ローカルのTIFFファイルを取得
            string[] localTif = GetImageName(LocalImagePath, "*.tif");
            
            FileFullPath = new string[localTif.Count()];
            FileCreationDate = new string[localTif.Count()];
            FileLength = new string[localTif.Count()];
            FileName = new string[localTif.Count()];

            for (int i = 0; i < localTif.Count(); i++)
            {
                FileFullPath[i] = localTif[i];
                FileName[i] = ProcessingPath(localTif[i], @"\", true, string.Empty);
                FileInfo info = new System.IO.FileInfo(localTif[i]);
                FileCreationDate[i] = info.CreationTime.ToString();
                var filesize = info.Length / 1024f;
                FileLength[i] = filesize.ToString("0");
            }
        }


        public string DownLoadFile(string targetFilePath, string serverFilePath, ref string ErrorMessageStr)
        {
            //ファイル名のみを取得
            string fileName = ProcessingPath(targetFilePath, @"\", true, string.Empty);
            //フルパスを作成
            string fullFileName = string.Concat(serverFilePath, @"\", fileName);
            //コピー先Dir
            DirectoryInfo serverDir = new DirectoryInfo(serverFilePath);

            //コピー前に存在確認
            if (!File.Exists(fullFileName))
            {
                try
                {
                    //ファイルコピー
                    FileInfo fileInfo = new FileInfo(targetFilePath);
                    fileInfo.CopyTo(Path.Combine(serverDir.FullName, fileInfo.Name), false);
                }
                catch (Exception e)
                {
                    ErrorMessageStr = e.Message.ToString();
                }
            }
            
            return fullFileName;
        }
        
        /// <summary>
        /// ディレクトリとファイル名を結合
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="partition"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string CreatePath(string dir,string partition, string fileName)
        {
            StringBuilder path = new StringBuilder();
            path.Append(dir);
            path.Append(partition);
            path.Append(ProcessingPath(fileName, @"\", true, string.Empty));
            return path.ToString();
        }

        /// <summary>
        /// Log内容取得
        /// </summary>
        public string GetLogFile(string LogPath, string ServerLogPath, ref string DebugStr)
        {
            RunTime = new List<string>();
            Client = new List<string>();
            Instruction = new List<string>();
            Status = new List<string>();
            LogHelper logHelper = new LogHelper();

            logHelper.GetLogFile(LogPath, ServerLogPath, ref RunTime, ref Client, ref Instruction, ref Status, ref DebugStr);
            return DebugStr;
        }
        
        /// <summary>
        /// 最新の画像ファイル名を取得
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        public string[] GetCurrentImageName(string localDirPath, string serverDirPath, ref string DebugStr)
        {
            GetLatestImage getNewestImage = new GetLatestImage();
            string[] images;

            if (!getNewestImage.GetGetCurrentImage(localDirPath, serverDirPath, ref DebugStr))
            {
                return images = new string[] { };
            }
            
            images = GetImageName(serverDirPath, "*.tif");
            return images;
        }

        /// <summary>
        /// 最新の画像ファイルを取得
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="ServerCurrentImagePath"></param>
        /// <param name="ServerContentPath"></param>
        /// <param name="png"></param>
        /// <returns></returns>
        public dynamic GetCurrentImage(string[] filePath, string ServerCurrentImagePath, string ServerContentPath)
        {
            List<string> png = new List<string>();

            foreach (var file in filePath)
            {
                png.Add(ConvertImg(file, ServerCurrentImagePath, ServerContentPath));
            }

            return png;
        }

        /// <summary>
        /// 最新のtif画像をpngに変換し保存
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="ServerCurrentImagePath"></param>
        /// <param name="ServerContentPath"></param>
        /// <returns></returns>
        private string ConvertImg(string imagePath, string ServerCurrentImagePath, string ServerContentPath)
        {
            string fileName = string.Empty;

            //imageオブジェクト
            using (System.Drawing.Image png = System.Drawing.Image.FromFile(imagePath))
            {
                //ファイル名だけ取得
                fileName = Path.ChangeExtension(ProcessingPath(imagePath, @"\", false), "jpg");
                
                //一時保存
                png.Save(ServerCurrentImagePath + fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            //Content配下に保存するためのpath
            return CreatePath(ServerContentPath, "/", fileName);
        }
        

        /// <summary>
        /// 最新作成日付のフォルダパスを取得
        /// </summary>
        /// <param name="path"></param>
        private void GetCurrentDir(ref string path, string targetDir)
        {
            //ディレクトリ一覧取得
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(targetDir));
            //比較用日付
            DateTime? currentDirDate = null;
            
            foreach (var dir in dirs)
            {
                DateTime createDate = Directory.GetCreationTime(dir);

                if (currentDirDate == null)
                {
                    currentDirDate = createDate;
                    continue;
                }
                if (Convert.ToDateTime(currentDirDate) < createDate)
                {
                    currentDirDate = createDate;
                    path = dir;
                }
            }
        }

        /// <summary>
        /// 画像ファイル名を取得
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        private string[] GetImageName(string path, string searchPattern)
        {
            string[] tifFiles = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
            return tifFiles;
        }

        /// <summary>
        /// 画像ファイル名を取得
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public string[] GetImageFileName(string[] paths)
        {
            string[] ImageFileNames = new string[paths.Count()];
            paths.CopyTo(ImageFileNames, 0);

            for (int i = 0; i < paths.Count(); i++)
            {
                ImageFileNames[i] = ProcessingPath(paths[i], @"\", true, string.Empty);
            }

            return ImageFileNames;
        }
        
        /// <summary>
        /// パスの加工
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indexSymbol"></param>
        /// <param name="replaceFlg"></param>
        /// <param name="modify"></param>
        /// <returns></returns>
        public string ProcessingPath(string path, string indexSymbol, bool replaceFlg, string modify = null)
        {
            string newPath = string.Empty;

            if (replaceFlg)
            {
                return newPath = (path.Remove(0, path.LastIndexOf(indexSymbol))).Replace(indexSymbol, modify);
            }

            return newPath = path.Remove(0, path.LastIndexOf(indexSymbol));
        }

        
    }
}