using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Settings
{
    public class GetPixelSize : NisAPIBase
    {
        private enum eQueryIndex
        {
            Precision = 0,
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.Precision,
                new IntValue()
                {
                    MinValue = 0,
                    isOption = true
                });

            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                // Get pixel size by um.

                ////////////////////////
                // get current settings
                NisMacro.Net.Macro.Macros.Calibration.GetCalibrationViewUnit getCalibUnit =
                    new NisMacro.Net.Macro.Macros.Calibration.GetCalibrationViewUnit();
                AddMacroToQueSync(getCalibUnit);

                ////////////////////////
                // Set Unit and Prec
                NisMacro.Net.Macro.Macros.Calibration.SetCalibrationViewUnit setCalibUnit =
                    new NisMacro.Net.Macro.Macros.Calibration.SetCalibrationViewUnit()
                    {
                        CalibrationViewUnit = "um"
                    };
                AddMacroToQueSync(setCalibUnit);

                int prec = 5;
                if(ParamValues[eQueryIndex.Precision].Value != null)
                {
                    prec = ParamValues[eQueryIndex.Precision].ToInt;
                }
                NisMacro.Net.Macro.Macros.Calibration.SetCalibrationViewPrecision setCalibPrec =
                    new NisMacro.Net.Macro.Macros.Calibration.SetCalibrationViewPrecision()
                    {
                        CalibrationViewPrecision = prec
                    };
                AddMacroToQueSync(setCalibPrec);

                ////////////////////////
                // GetCalibration
                NisMacro.Net.Macro.Macros.Calibration.Get_CurrentCalibration getCalib =
                   new NisMacro.Net.Macro.Macros.Calibration.Get_CurrentCalibration()
                   {
                       ActiveCamera = 0,    // primary
                       FQMode = 0           // current
                   };                                  
                AddMacroToQueSync(getCalib);


                // Restore
                // Set Unit and Prec
                setCalibUnit.CalibrationViewUnit = getCalibUnit.CalibrationViewUnit;
                AddMacroToQueSync(setCalibUnit);

        
                HttpResponse.ResponseParam.Add(getCalib.cal);
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
