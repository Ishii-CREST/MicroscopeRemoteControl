using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.HttpUtil.Data;
using HttpConnect.API.APIValue;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class ND_StimulationAppendPhase : NisAPIBase
    {

        public enum eQueryIndex
        {
            PhaseType = 0,
            TimeInterval,
            TimeDuration,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.PhaseType, new IntValue());
            ParamValues.Add(eQueryIndex.TimeInterval, new DoubleValue());
            ParamValues.Add(eQueryIndex.TimeDuration, new DoubleValue());
            CreateParamList(typeof(eQueryIndex));
        }

        protected override bool RunAPIImpl()
        {
            try
            {
                NisMacro.Net.Macro.Macros.ND.Stimulation.ND_StimulationAppendPhase macro
                    = new NisMacro.Net.Macro.Macros.ND.Stimulation.ND_StimulationAppendPhase()
                    {
                        PhaseType = ParamValues[eQueryIndex.PhaseType].ToInt,
                        ND_TimeDuration = ParamValues[eQueryIndex.TimeDuration].ToDouble,
                        ND_TimeInterval = ParamValues[eQueryIndex.TimeInterval].ToDouble
                    };

                //ND_StimulationAppendPhase
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
