using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NISweb.Models
{
    public static class FileCollection
    {
        /// <summary>
        /// ローカルのImageディレクトリパス
        /// </summary>
        public static string LocalImagePath { get; set; }
        /// <summary>
        /// サーバのImageディレクトリパス
        /// </summary>
        public static string ServerImagePath { get; set; }
        /// <summary>
        /// 画面表示用パス
        /// </summary>
        public static string ServerContentPath { get; set; }
        /// <summary>
        /// 監視対象Logフォルダ
        /// </summary>
        public static string LogPath { get; set; }
        /// <summary>
        /// Webサーバ側のLog格納フォルダ
        /// </summary>
        public static string ServerLogPath { get; set; }

        //----------------------------------------------------
        //画像一覧表示用
        //----------------------------------------------------
        
        /// <summary>
        /// 全ファイルのファイルパス
        /// </summary>
        public static string[] FileFullPath { get; set; }
        
        public static string[] FileCreationDate { get; set; }

        public static string[] FileName { get; set; }

        public static string[] FileLength { get; set; }

        /// <summary>
        /// 過去Logを消す
        /// </summary>
        public static void DeleteLog()
        {
            string[] files = Directory.GetFiles(ServerLogPath);
            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }


    public class FileOperator
    {
        /// <summary>
        /// ローカルのTIFファイル情報取得
        /// </summary>
        public void SuckFiles()
        {
            //ローカルのTIFFファイルを取得
            string[] localTif = Directory.GetFiles(FileCollection.LocalImagePath, "*.tif", SearchOption.AllDirectories);
            //サーバにTIFファイルがあるかどうか
            string[] serverTif = Directory.GetFiles(FileCollection.ServerImagePath, "*.tif", SearchOption.AllDirectories);

            //サーバ側のImageパス内にTIFファイルがあったら削除
            if (0 < serverTif.Count())
            {
                DeleteImage(serverTif);
            }
            

            FileCollection.FileFullPath = new string[localTif.Count()];
            FileCollection.FileCreationDate = new string[localTif.Count()];
            FileCollection.FileName = new string[localTif.Count()];
            FileCollection.FileLength = new string[localTif.Count()];

            for (int i = 0; i < localTif.Count(); i++)
            {
                //ローカルからサーバへファイルコピー
                File.Copy(localTif[i], CreatePath(FileCollection.ServerImagePath, @"\", localTif[i]), true);
                //画面表示用パスを生成
                FileCollection.FileFullPath[i] = CreatePath(FileCollection.ServerContentPath, "/", localTif[i]);
                
                FileInfo info = new System.IO.FileInfo(localTif[i]);
                FileCollection.FileCreationDate[i] = info.CreationTime.ToString();
                FileCollection.FileName[i] = info.Name.ToString();
                var filesize = info.Length / 1024f;
                FileCollection.FileLength[i] = filesize.ToString("0");
            }
        }

        private void DeleteImage(string[] files)
        {
            foreach (var file in files)
            {
                File.Delete(file);
            }
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
            path.Append(fileName.Remove(0,fileName.LastIndexOf(@"\") + 1));
            return path.ToString();
        }

        /// <summary>
        /// Logの中身取得
        /// </summary>
        public List<string> GetLogFile()
        {
            //ファイル名を取得
            string[] files = Directory.GetFiles(FileCollection.LogPath, "*.log", SearchOption.AllDirectories);

            //読み取ったlogの中身
            List<string> fileContent = new List<string>();

            if (0 < files.Count())
            {
                //コピー時ファイル名作成
                string logFileName = string.Format(@"{0}\{1}.log", FileCollection.ServerLogPath, DateTime.Now.ToString("yyyyMMdd"));
                
                foreach (var file in files)
                {
                    File.Copy(file, logFileName, true);
                }
                
                //log内容読み取り
                using (StreamReader reader = new StreamReader(logFileName, System.Text.Encoding.UTF8))
                {
                    for (int i = 0; i < reader.Peek(); i++)
                    {
                        fileContent.Add(reader.ReadLine());
                    }
                }
            }
            return fileContent;
        }
    }
}