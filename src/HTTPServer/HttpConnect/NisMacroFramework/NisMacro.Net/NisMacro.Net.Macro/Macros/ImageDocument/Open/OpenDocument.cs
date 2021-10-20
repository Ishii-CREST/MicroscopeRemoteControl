using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro.Macros.ImageDocument.Open
{
    /// <summary>
    /// The OpenDocument function opens the document and displays specified image on the screen.
    /// </summary>
    public class OpenDocument : NisMacroBase
    {
        /// <summary>
        /// Constractor
        /// </summary>
        public OpenDocument() : base()
        {
            this.MacroName = "OpenDocument";
        }

        /// <summary>
        /// Complete path to the the image.
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 1, true )]
        public string Image;

        /// <summary>
        /// SaveModified Specifies whether to save an opened document when it has been modified.
        /// 1: Asks whether to save changes or not.
        /// 2: Changes are saved.
        /// 3: Changes are not saved.
        /// </summary>
        [NisMacroAttribute( eParamType.InParam, 2 )]
        public int SaveModified;

        /// <summary>
        /// No Difine in this Macro.
        /// </summary>
        [NisMacroAttribute( eParamType.Return_Ignore, 0 )]
        public int Result;
    }
}
