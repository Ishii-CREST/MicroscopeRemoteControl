using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.HttpUtil.Data;
using HttpConnect.API.APIValue;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class ND_AppendTimePhaseEx : NisAPIBase
    {
        public enum eQueryIndex
        {
            TimeInterval = 0,
            TimeCount,
            TimeCommand,
            PhaseName,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.TimeInterval, new DoubleValue());
            ParamValues.Add(eQueryIndex.TimeCount, new IntValue());
            ParamValues.Add(eQueryIndex.TimeCommand, new StringValue() { isOption = true });
            ParamValues.Add(eQueryIndex.PhaseName, new StringValue() { isOption = true });
            CreateParamList(typeof(eQueryIndex));
        }

    protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_AppendTimePhaseEx macro
                    = new NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_AppendTimePhaseEx()
                    {
                        ND_PhaseName = ParamValues[eQueryIndex.PhaseName].ToStr,
                        ND_TimeCommand = ParamValues[eQueryIndex.TimeCommand].ToStr,
                        ND_TimeCount = ParamValues[eQueryIndex.TimeCount].ToInt,
                        ND_TimeInterval = ParamValues[eQueryIndex.TimeInterval].ToDouble
                    };

                //ND_AppendTimePhaseEx
                AddMacroToQueSync(macro);

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
    }
}
