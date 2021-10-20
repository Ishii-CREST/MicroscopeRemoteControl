using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.HttpUtil.Data
{
    /// <summary>
    /// HTTPのレスポンスデータ
    /// </summary>
    public class HttpResponseData
    {
        /// ステータスコード
        /// </summary>
        public CommonDefine.eHttpStatusCode ResultStatusCode { get; set; }

        /// <summary>
        /// レスポンスパラメータ
        /// </summary>
        public List<object> ResponseParam { get; set; } = new List<object>();
        /// <summary>
        /// レスポンスパラメータの取得
        /// </summary>
        public string ResultMessage
        {
            get
            {
                // カンマで繋げて取得する。
               return string.Join(",", ResponseParam.ToArray());
            }
        }
    }
}
