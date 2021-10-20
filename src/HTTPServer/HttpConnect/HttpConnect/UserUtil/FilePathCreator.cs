using System;
using System.IO;


namespace HttpConnect
{
    public class FilePathCreator
    {
        #region property
        #endregion

        #region method
        /// <summary>
        /// ファイル名(パス)作成
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="extention">拡張子(.抜き)</param>
        public static FileInfo CreateCurrentDateTimeFilePath(string prefix, string extention, out DateTime createDate)
        {
            // 現在日付のディレクトリを作製
            var saveFileDir = CreateCurrentDateTimeDir(out DateTime date).FullName;
            createDate = date;
            //suffix未設定時のnd2・tifファイル名作成
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = date.ToString(CommonDefine.DATETIME_FORMAT);
            }

            return new FileInfo(Path.Combine(saveFileDir, prefix + "." + extention));
        }

       public static DirectoryInfo CreateCurrentDateTimeDir(out DateTime createdDateTime)
        {
            createdDateTime = DateTime.Now;
            var saveFileDir = Path.Combine(RequestRelated.Instance.ImageDir, createdDateTime.ToString(CommonDefine.DATETIME_FORMAT));
            return Directory.CreateDirectory(saveFileDir);
        }

    }
            #endregion
}
