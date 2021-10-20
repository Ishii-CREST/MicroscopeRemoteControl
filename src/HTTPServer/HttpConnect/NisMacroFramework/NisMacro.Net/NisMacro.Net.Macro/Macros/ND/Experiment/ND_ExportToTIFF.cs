using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ND.Experiment
{
    /// <summary>
    /// ND_ExportToTIFF
    /// </summary>
    public class ND_ExportToTIFF : NisMacroBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ND_ExportToTIFF()
            : base()
        {
            this.MacroName = "ND_ExportToTIFF";
        }

        /// <summary>
        /// デフォルトパラメータ指定
        /// </summary>
        internal override void SetDefaultParamater()
        {
            this.Filename = string.Empty;
            this.Dest_Directory = string.Empty;
            this.Prefix = string.Empty;
            //this.ChannelExportType = string.Empty;
            //this.uiSaveAsMCH = string.Empty;
            //this.iMultiPageMask = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, true )]
        public string Filename;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2, true )]
        public string Dest_Directory;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 3, true )]
        public string Prefix;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 4, false)]
        public int ChannelExportType;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 5, false)]
        public int ApplyLuts;

        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 6, false)]
        public int uiSaveAsMCH;


        /// <summary>
        /// 
        /// </summary>
        [NisMacroAttribute(eParamType.InParam, 7, false)]
        public int iMultiPageMask;

        
        /// <summary>
        /// 結果(このマクロでは使用しない)
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
