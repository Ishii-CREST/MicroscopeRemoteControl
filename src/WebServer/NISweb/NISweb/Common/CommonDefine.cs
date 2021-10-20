using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NISweb.Common
{
    public static class CommonDefine
    {
        /// <summary>
        /// Log保存時の日付フォーマット
        /// </summary>
        public const string DATE_FORMAT = "yyyyMMdd";
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
        /// API名称  XYステージ座標
        /// </summary>
        public const string StgGetPosXY = "StgGetPosXY";
        /// <summary>
        /// API名称  Zステージ座標
        /// </summary>
        public const string StgGetPosZ = "StgGetPosZ";
        /// <summary>
        /// API名称  ND_IsInExperimentCapture
        /// </summary>
        public const string ND_IsInExperimentCapture = "ND_IsInExperimentCapture";
    }
}