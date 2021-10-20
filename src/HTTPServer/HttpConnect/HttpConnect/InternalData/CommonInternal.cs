using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.InternalData
{
    public class CommonInternal
    {
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CommonInternal myInstance = new CommonInternal();
        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static CommonInternal Instance { get { return myInstance; } }
        /// <summary>
        /// APIのログ
        /// </summary>
        public LogManage APILogManage { get; set; }
        /// <summary>
        /// アプリのログ
        /// </summary>
        public LogManage AppLogManage { get; set; }

    }
}
