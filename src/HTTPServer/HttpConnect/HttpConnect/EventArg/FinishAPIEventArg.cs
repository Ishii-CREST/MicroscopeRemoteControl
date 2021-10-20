using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.EventArg
{
    /// <summary>
    /// API受信イベントパラメータ
    /// </summary>
    public class FinishAPIEventArg:EventArgs
    {
        /// <summary>
        /// 受信したリクエストデータ
        /// </summary>
        public ReceivedRequestData ReceivedData { get; set; }
        /// <summary>
        /// 実行結果
        /// </summary>
        public HttpResponseData ResponseData { get; set; }
    }
}
