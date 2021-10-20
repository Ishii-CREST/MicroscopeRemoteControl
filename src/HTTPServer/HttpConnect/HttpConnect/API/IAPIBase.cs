using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API
{
    /// <summary>
    /// APIベースクラス
    /// </summary>
    public interface IAPIBase
    {
        /// <summary>
        /// API名の取得
        /// </summary>
        /// <returns></returns>
        string GetAPIName();

        /// <summary>
        /// リクエストパラメータの解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool AnalyzeRequestParam(ReceivedRequestData data);

        /// <summary>
        /// APIの実行
        /// </summary>
        /// <returns></returns>
        bool RunAPI();

        /// <summary>
        /// 実行結果の取得
        /// </summary>
        /// <returns></returns>
        HttpResponseData GetResponseData();
    }
}
