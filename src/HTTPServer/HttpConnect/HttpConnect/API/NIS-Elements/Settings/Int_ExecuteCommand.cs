using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.HttpUtil.Data;
using HttpConnect.API.APIValue;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class Int_ExecuteCommand : NisAPIBase
    {
        public enum eQueryIndex
        {
           AppID = 0,
           Command
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.AppID, new IntValue());
            ParamValues.Add(eQueryIndex.Command, new StringValue());
            CreateParamList(typeof(eQueryIndex));
        }

        protected override bool RunAPIImpl()
        {
            
            try
            {
                // PIDの指定が負の数の場合は現在実行中のNISに行う。
                int nisPID = ParamValues[eQueryIndex.AppID].ToInt;
                if (nisPID < 0)
                {
                    nisPID = NisMacro.Net.Util.NisArMonitor.Instance.GetNisArProcess.Id;
                }
                NisMacro.Net.Macro.Macros.AdvancedAPI.InterProcess.Int_ExecuteCommand macro
                    = new NisMacro.Net.Macro.Macros.AdvancedAPI.InterProcess.Int_ExecuteCommand()
                    {
                        AppId = nisPID,
                        Command = ParamValues[eQueryIndex.Command].ToStr
                    };

                //ND_AppendTimePhaseEx
                AddMacroToQueSync(macro);

                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;
                return true;
            }
            catch (Exception e)
            {
                OutputLog("", e);
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                return false;
            }
        }
    }
}
