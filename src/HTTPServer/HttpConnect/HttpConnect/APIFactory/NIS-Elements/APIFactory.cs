using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.APIUtil.NIS_Elements
{
    /// <summary>
    /// NIS-API取得クラス(シングルトン)
    /// </summary>
    public class APIFactrory:IAPIFactory
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static APIFactrory myInstance = new APIFactrory();
        public static APIFactrory Instance { get { return myInstance; } }
        /// <summary>
        /// API Dictionary
        /// </summary>
        private Dictionary<string, Type> ApiDic = new Dictionary<string, Type>();
        private APIFactrory()
        {
            // NIS_Elements.NisApiBase を継承した全クラスを取得する
            var apiList = Assembly.GetAssembly(typeof(API.NIS_Elements.NisAPIBase)).GetTypes().
                Where(t =>
                {
                    return t.IsSubclassOf(typeof(API.NIS_Elements.NisAPIBase));
                });

            // 全クラスの持つAPI名と型を保存
            foreach(var val in apiList)
            {
                ApiDic.Add(((API.NIS_Elements.NisAPIBase)val.GetConstructor(Type.EmptyTypes).Invoke(null)).APIName,
                    val.GetTypeInfo());
            }
        }

        /// <summary>
        /// API名に合致したAPIクラスを取得する
        /// </summary>
        /// <param name="apiName">API名</param>
        /// <returns></returns>
        public API.IAPIBase GetAPI(string apiName)
        {
            if (ApiDic.ContainsKey(apiName))
            {
                // 合致するAPIがあった場合、コンストラクタを実行してインスタンスを返す。
                return (API.IAPIBase)ApiDic[apiName].GetConstructor(Type.EmptyTypes).Invoke(null);
            }
            return null;
        }

    }
}
