using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.HttpUtil.Data;
using HttpConnect.API.APIValue;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class ND_SetZSeriesExp : NisAPIBase
    {
        public enum eQueryIndex
        {
            ZSeriesType = 0,
            ZSeriesTop,
            ZSeriesHome,
            ZSeriesBottom,
            ZSeriesStep,
            ZSeriesCount,
            ZSeriesHomeDefined,
            ZSeriesCloseShuter,
            ZSeriesDevice,
            TimeCommandBefore,
            TimeCommandAfter,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.ZSeriesType, new IntValue());
            ParamValues.Add(eQueryIndex.ZSeriesTop, new DoubleValue());
            ParamValues.Add(eQueryIndex.ZSeriesHome, new DoubleValue());
            ParamValues.Add(eQueryIndex.ZSeriesBottom, new DoubleValue());
            ParamValues.Add(eQueryIndex.ZSeriesStep, new DoubleValue());
            ParamValues.Add(eQueryIndex.ZSeriesCount, new IntValue());
            ParamValues.Add(eQueryIndex.ZSeriesHomeDefined, new IntValue());
            ParamValues.Add(eQueryIndex.ZSeriesCloseShuter, new IntValue());
            ParamValues.Add(eQueryIndex.ZSeriesDevice, new StringValue() {  isOption = true});
            ParamValues.Add(eQueryIndex.TimeCommandBefore, new StringValue() { isOption = true});
            ParamValues.Add(eQueryIndex.TimeCommandAfter, new StringValue() { isOption = true});
            CreateParamList(typeof(eQueryIndex));
        }

        protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.ND.ZSeries.ND_SetZSeriesExp macro
                    = new NisMacro.Net.Macro.Macros.ND.ZSeries.ND_SetZSeriesExp()
                    {
                        ND_LpZSeriesTop = ParamValues[eQueryIndex.ZSeriesTop].ToDouble,
                        ND_LpZSeriesBottom = ParamValues[eQueryIndex.ZSeriesBottom].ToDouble,
                        ND_LpZSeriesCloseShuter = ParamValues[eQueryIndex.ZSeriesCloseShuter].ToInt,
                        ND_LpZSeriesCount = ParamValues[eQueryIndex.ZSeriesCount].ToInt,
                        ND_LpZSeriesHome = ParamValues[eQueryIndex.ZSeriesHome].ToDouble,
                        ND_LpZSeriesHomeDefined = ParamValues[eQueryIndex.ZSeriesHomeDefined].ToInt,
                        ND_LpZSeriesStep = ParamValues[eQueryIndex.ZSeriesStep].ToDouble,
                        ND_LpZSeriesType = ParamValues[eQueryIndex.ZSeriesType].ToInt,
                        ND_TimeCommandAfter = ParamValues[eQueryIndex.TimeCommandAfter].ToStr,
                        ND_TimeCommandBefore = ParamValues[eQueryIndex.TimeCommandBefore].ToStr,
                        ND_ZSeriesDevice = ParamValues[eQueryIndex.ZSeriesDevice].ToStr,
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
