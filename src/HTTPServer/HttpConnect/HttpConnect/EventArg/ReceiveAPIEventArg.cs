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
    public class ReceiveAPIEventArg:EventArgs
    {
        /// <summary>
        /// 受信したリクエストデータ
        /// </summary>
        public ReceivedRequestData ReceivedData { get; set; }

    }
}
