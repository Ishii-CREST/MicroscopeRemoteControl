using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect
{
    public class CommonDefine
    {
        /// <summary>
        /// ステータスコード
        /// </summary>
        public enum eHttpStatusCode : int
        {
            OK = 200,
            BadRequest = 400,
            InternalServerError = 500
        }
    }
}
