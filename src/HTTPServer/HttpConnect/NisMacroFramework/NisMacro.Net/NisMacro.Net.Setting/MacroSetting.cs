using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Setting
{
    public class MacroSetting
    {
        #region ----- シングルトンコード ------
        private static MacroSetting myInstance = new MacroSetting();
        private MacroSetting()
        {
            MacroIncludeFilePath = DefaultSetting.INCLUDE_FILE_PATH;
        }
        #endregion

        /// <summary>
        /// マクロ実行時のインクルードファイルパス
        /// </summary>
        public string MacroIncludeFilePath { get; set; }

        public static MacroSetting Instance {
            get { return myInstance; }
        }
    }
}
