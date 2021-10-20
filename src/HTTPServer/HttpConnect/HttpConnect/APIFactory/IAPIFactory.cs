using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.APIUtil
{
    interface IAPIFactory
    {
        /// <summary>
        /// API名に合致したAPIを取得する
        /// </summary>
        /// <param name="apiName"></param>
        /// <returns></returns>
        API.IAPIBase GetAPI(string apiName);
    }
}
