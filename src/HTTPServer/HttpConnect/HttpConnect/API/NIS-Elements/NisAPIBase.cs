using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpConnect.API.NIS_Elements
{
    public class NisAPIBase : IAPIBase
    {
        /// <summary>
        /// リクエストパラメータ
        /// </summary>
        protected Dictionary<Enum, APIValueBase> ParamValues = new Dictionary<Enum, APIValueBase>();
        /// <summary>
        /// HTTPのリクエストを解析済みか否か
        /// </summary>
        protected bool isAnalyzedRequest { get; set; }
        /// <summary>
        /// HTTP結果
        /// </summary>
        public HttpResponseData HttpResponse { get; set; } = new HttpResponseData();
        /// <summary>
        /// リクエストデータ
        /// </summary>
        public ReceivedRequestData HttpReceivedData { get; set; }
        /// <summary>
        /// HTTPレスポンス
        /// </summary>
        /// <returns></returns>
        public HttpResponseData GetResponseData() { return HttpResponse; }
        /// <summary>
        /// 必須パラメータ数
        /// </summary>
        protected int ParamMaxCount { get; set; }
        /// <summary>
        /// パラメータ順序定義Enum
        /// </summary>
        protected List<Enum> ParamIndexEnumArray { get; set; } = new List<Enum>();

        /// <summary>
        /// ※引数つきコンストラクタは作製不可とします。
        /// </summary>
        public NisAPIBase()
        {
            DefineAPIParams();

            // オプションではないパラメータの和を取得する
            ParamMaxCount = new List<APIValueBase>(ParamValues.Values).FindAll(d => !d.isOption).Count;

            // 基本的にクラス名=API名として設定する。
            APIName = this.GetType().Name;
        }

        /// <summary>
        /// API名
        /// </summary>
        public string APIName { get; set; }
        /// <summary>
        /// マクロ名設定
        /// </summary>
        /// <returns></returns>
        public string GetAPIName()
        {
            return this.APIName;
        }
        /// <summary>
        /// APIパラメータ定義
        /// </summary>
        protected virtual void DefineAPIParams() { }
        /// API処理実行
        /// </summary>
        public bool RunAPI()
        {
            if (!isAnalyzedRequest)
            {
                OutputLog("Request parameter was not analyzed. API can not run.");
                return false;
            }
            return RunAPIImpl();
        }
        /// <summary>
        /// リクエストデータの解析
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool AnalyzeRequestParam(ReceivedRequestData data)
        {
            if (AnalyzeRequestParamImpl(data))
            {
                isAnalyzedRequest = true;
                HttpReceivedData = data;
                return true;
            }
            else
            {
                OutputLog("Failed to analyze request parameter.");
                return false;
            }
        }

        /// <summary>
        /// ※各APIクラスにて実装
        /// API実行処理本体
        /// </summary>
        /// <returns></returns>
        protected virtual bool RunAPIImpl()
        {
            return true;
        }

        /// <summary>
        /// 各APIクラスにて実装
        /// HTTPリクエストデータ解析本体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected bool AnalyzeRequestParamImpl(ReceivedRequestData data)
        {
            var query = data.GetQueryArgList();     // クエリ文字列を取得

            if (query.Count < ParamMaxCount)
            {
                // 必須パラメータ数に達して居なければエラー
                OutputLog(string.Format("Parameter is too less. required = [{0}]", ParamMaxCount));
                return false;
            }
            // クエリを解析
            for (int i = 0; i < query.Count; i++)
            {
                int index =  ParamIndexEnumArray.FindIndex(d => Convert.ToInt32(d)== i);
                if(index != -1)
                {
                    if (!ParamValues[ParamIndexEnumArray[index]].AnalyzeParam(query[i]))
                    {
                        OutputLog(string.Format("Value [{0}] is mismatch type.", query[i]));
                        return false;
                    }
                    OutputLog(string.Format("Parameter analyzed.[{0}][{1}]", ParamIndexEnumArray[index], ParamValues[ParamIndexEnumArray[index]].Value));
                }
            }

            return true;
        }

        /// <summary>
        /// リクエストデータの不足をチェックする
        /// </summary>
        /// <param name="data"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        protected bool CheckRequestParamNum(List<string> data, int max)
        {
            if (data.Count < max)
            {
                OutputLog(string.Format("Request parameter num is not enough.(less than {0})", max));
                return false;
            }
            return true;
        }

        /// <summary>
        /// パラメータのEnumからパラメータの配列を作成する。
        /// </summary>
        /// <param name="ParamEnum"></param>
        protected void CreateParamList(Type ParamEnum)
        {
            ParamIndexEnumArray = Enum.GetValues(ParamEnum).Cast<Enum>().ToList();
        }
        /// <summary>
        /// 同期でマクロを実行する
        /// </summary>
        /// <param name="macro"></param>
        protected void AddMacroToQueSync(NisMacro.Net.Macro.Macros.NisMacroBase macro)
        {
            NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(macro);
        }
        /// <summary>
        /// 非同期でマクロを実行キューに追加する
        /// </summary>
        /// <param name="macro"></param>
        protected void AddMacroToQueAsync(NisMacro.Net.Macro.Macros.NisMacroBase macro)
        {
            NisMacro.Net.Execute.MacroQueue.GetInstance.AddMacro(macro);
        }

        /// <summary>
        /// APPログ出力を行う。
        /// </summary>
        /// <param name="message"></param>
        protected void OutputLog(string message)
        {
            InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format("API[{0}]:[{1}]", this.APIName, message));
        }
        /// <summary>
        /// APPログ出力を行う。
        /// </summary>
        /// <param name="message"></param>
        protected void OutputLog(string message, Exception ex)
        {
            OutputLog(string.Format("{0}" + System.Environment.NewLine + "{1}{2}", message, ex.Message, ex.StackTrace));
        }
    }
}
