using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using HttpConnect.API;
using HttpConnect.HttpUtil.Data;

namespace HttpConnect.HttpUtil
{
    /// <summary>
    /// クライアントPCからのHttpRequest受信処理クラス
    /// </summary>
    public class HttpIntercept : IDisposable
    {
        public delegate void _OnReceiveAPI(EventArg.ReceiveAPIEventArg arg);
        /// <summary>
        /// API受信時のイベント
        /// </summary>
        public event _OnReceiveAPI OnReceiveAPI;

        public delegate void _OnFinishAPI(EventArg.FinishAPIEventArg arg);
        /// <summary>
        /// API完了時のイベント
        /// </summary>
        public event _OnFinishAPI OnFinishAPI;

        #region 変数
        /// <summary>
        /// HTTPリクエストの受信処理を担うリスナー
        /// </summary>
        private HttpListener Listener { get; set; }
        private bool waitReqFlag = false;
        private APIUtil.IAPIFactory ApiFactory;

        #endregion

        #region constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HttpIntercept()
        {
            Listener = new HttpListener();
        }
        #endregion

        #region method

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <returns></returns>
        public bool Initialize ()
        {
            try
            {
                Initialize(CommonDefine.eMicroscopeSoftware.NIS_Elements);     // APIの取得先を変更(現在はNIS-Elements固定)
                                                                               //HTTPリクエストを受信するListenerの起動準備
                                                                               //受信対象URLの設定
                Listener.Prefixes.Add(string.Format("http://*:{0}/", RequestRelated.Instance.Port));
                //Listener起動
                Listener.Start();
                if (Listener.IsListening)
                {
                    APPLog(string.Format("#### HTTP Listen Start. ####"));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// HTTPサーバ起動
        /// </summary>
        public bool StartReceive()
        {
            try
            {
                //HTTPリクエスト受信処理からレスポンス送信までを行うプロセス本体の起動
                waitReqFlag = true;
                GetRequest();
                return true;
            }
            catch (Exception e)
            {
                //HTTPサーバ起動失敗時にエラー内容を表示
                MessageBox.Show(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 終了時処理
        /// </summary>
        public void EndProc()
        {
            waitReqFlag = false;
            Listener.Stop();
            Listener.Close();
            APPLog(string.Format("#### HTTP Listen Stop. ####"));
        }
        private void Initialize(CommonDefine.eMicroscopeSoftware target)
        {
            // 今の所NISのみ
            if (target == CommonDefine.eMicroscopeSoftware.NIS_Elements)
            {
                // NIS-ElementsのAPIを取得できるようにする
                ApiFactory = APIUtil.NIS_Elements.APIFactrory.Instance;
                APPLog(string.Format("#### Initialize System for NIS-Elements. ####"));
            }
        }
        /// <summary>
        /// HTTPリクエスト受信待機
        /// </summary>
        private void GetRequest()
        {
            //API実行後、受信待機状態に戻る
            while (waitReqFlag)
            {
                //非同期として受信処理
                IAsyncResult result
                    = Listener.BeginGetContext(new AsyncCallback(ProcessingOnReceive), Listener);

                result.AsyncWaitHandle.WaitOne();
                System.Threading.Thread.Sleep(1);
            }
        }

        /// <summary>
        /// HTTPリクエスト受信処理
        /// </summary>
        /// <param name="result"></param>
        private void ProcessingOnReceive(IAsyncResult result)
        {
            if (!Listener.IsListening) return;

            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);

            HttpResponseData responseData = new HttpResponseData();
            ReceivedRequestData receivedData = new ReceivedRequestData();
            try
            {
                HttpListenerRequest request = context.Request;

                //リクエストがPOSTなのでstreamを生成して中身を取り出す
                receivedData.SetQuery(request);    // 受信データ生成

                APPLog(string.Format("HTTP Req Received URL:[{0}]", context.Request.Url));
                APPLog(string.Format("HTTP Req Received PARAMS:[{0}]", receivedData.QueryString));

                // API受信イベント
                if (OnReceiveAPI != null)
                {
                    OnReceiveAPI(new EventArg.ReceiveAPIEventArg() { ReceivedData = receivedData });
                }

                // マクロ検索・実行
                // マクロ取得
                API.IAPIBase api = this.ApiFactory.GetAPI(receivedData.APIName);
                if (api != null)
                {
                    APPLog(string.Format("---- Start API:[{0}] Params[{1}] ----", api.GetAPIName(), receivedData.QueryString));
                    if (!api.AnalyzeRequestParam(receivedData))
                    {
                        // リクエストデータの解析に失敗
                        responseData.ResultStatusCode = CommonDefine.eHttpStatusCode.BadRequest;
                        APILog(receivedData, " Invalid API Parameter.");
                    }
                    else
                    {
                        APILog(receivedData, "Start");
                        //Log
                        if (!api.RunAPI())
                        {
                            // マクロ実行に失敗
                            responseData.ResultStatusCode = CommonDefine.eHttpStatusCode.BadRequest;
                            InternalData.CommonInternal.Instance.APILogManage.OutputLog(receivedData.ClientIPAddress, receivedData.APIName, "Failed");
                            APPLog(string.Format("---- Failed API:[{0}] ----", api.GetAPIName()));
                        }
                        else
                        {
                            // マクロ成功
                            responseData = api.GetResponseData();
                            InternalData.CommonInternal.Instance.APILogManage.OutputLog(receivedData.ClientIPAddress, receivedData.APIName, "Success");
                            APPLog(string.Format("---- Success API:[{0}] ----", api.GetAPIName()));
                        }
                    }
                }
                else
                {
                    // 一致APIなし。
                    //いずれにも該当しない場合はリクエスト不正のステータスを設定する
                    responseData.ResultStatusCode = CommonDefine.eHttpStatusCode.BadRequest;

                    APILog(receivedData, " Invalid API Name.");
                    APPLog(string.Format("---- Invalid API:[{0}] ----", receivedData.APIName));
                }
            }
            catch (Exception e)
            {
                //ステータスコード設定
                responseData.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                //ログ出力
                APPLog("", e);
            }

            ////////////////
            // レスポンス送信
            SendResponse(context, responseData);
            // API受信イベント
            if (OnFinishAPI != null)
            {
                OnFinishAPI(new EventArg.FinishAPIEventArg() { ReceivedData = receivedData , ResponseData = responseData});
            }
        }

        /// <summary>
        /// レスポンスの送信
        /// </summary>
        /// <param name="context"></param>
        /// <param name="respData"></param>
        private void SendResponse(HttpListenerContext context, HttpResponseData respData)
        {
            try
            {
                if (context == null)
                {
                    //　送れない
                    InternalData.CommonInternal.Instance.AppLogManage.OutputLog("HTTP Listener context is null. Cannot send response.");
                }
                else
                {
                    using (HttpListenerResponse resp = context.Response)
                    {
                        resp.StatusCode = (int)respData.ResultStatusCode;

                        // パラメータがある場合は文字列を取得する
                        if (respData.ResponseParam.Count > 0)
                        {
                            byte[] buffer = Encoding.UTF8.GetBytes(respData.ResultMessage);
                            resp.ContentLength64 = buffer.Length;
                            System.IO.Stream output = resp.OutputStream;
                            output.Write(buffer, 0, buffer.Length);
                        }
                    }
                    InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("HTTP Response send.  Status[{0}]  Params[{1}]",
                                                                  respData.ResultStatusCode,
                                                                  respData.ResultMessage));
                }
            }
            catch (Exception ex)
            {
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog("", ex);
            }
        }

        /// <summary>
        /// APIのログを出力する
        /// </summary>
        /// <param name="receivedData"></param>
        /// <param name="message"></param>
        private void APILog(ReceivedRequestData receivedData, string message)
        {
            InternalData.CommonInternal.Instance.APILogManage.OutputLog(receivedData.ClientIPAddress, receivedData.APIName, message);
        }

        /// <summary>
        /// APPのログを出力する
        /// </summary>
        /// <param name="receivedData"></param>
        /// <param name="message"></param>
        private void APPLog( string message)
        {
            InternalData.CommonInternal.Instance.AppLogManage.OutputLog(message);
        }

        /// <summary>
        /// APPのログを出力する
        /// </summary>
        /// <param name="receivedData"></param>
        /// <param name="message"></param>
        private void APPLog(string message, Exception ex)
        {
            InternalData.CommonInternal.Instance.AppLogManage.OutputLog(message, ex);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 重複する呼び出しを検出するには

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    EndProc();
                }
                disposedValue = true;
            }
        }

        // ~HttpIntercept() {
        //   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
        //   Dispose(false);
        // }

        // このコードは、破棄可能なパターンを正しく実装できるように追加されました。
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
            Dispose(true);
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion
    }
}