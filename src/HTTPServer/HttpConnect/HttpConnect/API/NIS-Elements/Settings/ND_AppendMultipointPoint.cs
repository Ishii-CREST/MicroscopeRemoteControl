using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpConnect.HttpUtil.Data;
using HttpConnect.API.APIValue;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class ND_AppendMultipointPoint : NisAPIBase
    {
        public enum eQueryIndex
        {
            XCoord = 0,
            YCoord,
            ZCoord,
            PointName,
            
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.XCoord, new DoubleValue());
            ParamValues.Add(eQueryIndex.YCoord, new DoubleValue());
            ParamValues.Add(eQueryIndex.ZCoord, new DoubleValue());
            ParamValues.Add(eQueryIndex.PointName, new StringValue() { isOption = true });
            CreateParamList(typeof(eQueryIndex));
        }
        protected override bool RunAPIImpl()
        {
            
            try
            {
                NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_AppendMultipointPoint macro
                    = new NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_AppendMultipointPoint()
                    {
                           ND_MultipointName = ParamValues[eQueryIndex.PointName].ToStr,
                           ND_MultipointXCoord = ParamValues[eQueryIndex.XCoord].ToDouble,
                           ND_MultipointYCoord = ParamValues[eQueryIndex.YCoord].ToDouble,
                           ND_MultipointZCoord = ParamValues[eQueryIndex.ZCoord].ToDouble
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
