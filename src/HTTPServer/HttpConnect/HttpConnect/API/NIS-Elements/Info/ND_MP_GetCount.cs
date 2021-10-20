﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class ND_MP_GetCount : NisAPIBase
    {
        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_MP_GetCount macro
                    = new NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_MP_GetCount();

                //ND_MP_GetCount実行
                AddMacroToQueSync(macro);

                HttpResponse.ResponseParam.Add(macro.Result);
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                OutputLog("", e);
                return false;
            }
        }
        #endregion
    }
}
