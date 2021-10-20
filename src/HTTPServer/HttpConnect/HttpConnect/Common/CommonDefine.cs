using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect
{
    public static class CommonDefine
    {
        /// <summary>
        /// 画像保存時の日付フォーマット
        /// </summary>
        public const string DATETIME_FORMAT = "yyyyMMdd_HHmmss";
        /// <summary>
        /// 画像転送用ディレクトリ名
        /// </summary>
        public const string TRANS_IMAGE_TEMP_DIRNAME = "TransImageTemp";
        /// <summary>
        /// 画像ファイル　Time 　Prefix
        /// </summary>
        public const string FILE_T_PEFIX = "t";
        /// <summary>
        /// 画像ファイル　XY Prefix
        /// </summary>
        public const string FILE_XY_PEFIX = "xy";
        /// <summary>
        /// 画像ファイル　Z　Prefix
        /// </summary>
        public const string FILE_Z_PEFIX = "z";
        /// <summary>
        /// 画像ファイル　Ch　Prefix
        /// </summary>
        public const string FILE_CH_PEFIX = "c";
        /// <summary>
        /// コピー元ファイル保存先フォルダ名
        /// </summary>
        public const string DIR_ORG_FILE = "OrgFile";
        /// <summary>
        /// Web.configファイル名
        /// </summary>
        public const string WEB_CONFIG = "web.config";
        /// <summary>
        /// ステータスコード
        /// </summary>
        public enum eHttpStatusCode : int
        {
            OK = 200,
            Accepted = 202,
            BadRequest = 400,
            NotFound = 404,
            RequestTimeOut=408,
            InternalServerError = 500
        }
        public enum eMicroscopeSoftware
        {
            NIS_Elements = 0,
        }
    }
}
