using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements
{
    public static class NisAPIUtil
    {
        public static InternalData.NIS_Elements.NDExperimentInfo GetNDExpInfo()
        {
            InternalData.NIS_Elements.NDExperimentInfo retInfo = new InternalData.NIS_Elements.NDExperimentInfo();
            //計算に使用する各要素
            List<int> culcElement = new List<int>();

            var checkEnable = new NisMacro.Net.Macro.Macros.Acquire.ND_IsAcqTabChecked();

            ////////////////////////////
            //  Time Count
            //Timeが有効か調べる
            checkEnable.NDAcquisitionTabName = "Time";
            NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(checkEnable);
            retInfo.enableT = checkEnable.Result == 1 ? true : false;
            if (checkEnable.Result == 1)
            {
                //Time数取得
                NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_GetTimeLapsePhaseCount timeCount
                    = new NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_GetTimeLapsePhaseCount();

                NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(timeCount);

                //Time数保持
                //culcElement.Add(timeCount.Result);

                //Loop
                int loop = 0;

                for (int i = 0; i < timeCount.Result; i++)
                {

                    NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_GetTimePhaseSchedule timeSche
                        = new NisMacro.Net.Macro.Macros.ND.TimeLapse.ND_GetTimePhaseSchedule()
                        {
                            ND_TimePhase = i
                        };

                    NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(timeSche);

                    //loop数を取得
                    loop += timeSche.ND_TimeLoopCnt;

                    retInfo.loopT.Add(timeSche.ND_TimeLoopCnt);
                }
                //loop数保持
                culcElement.Add(loop);
            }
            /////////////////////////
            // XY
            //XYが有効か調べる
            checkEnable.NDAcquisitionTabName = "XY";
            NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(checkEnable);
            retInfo.enableXY = checkEnable.Result == 1 ? true : false;
            if (checkEnable.Result == 1)
            {
                //XY数を取得
                NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_MP_GetCount multiPointCount
                    = new NisMacro.Net.Macro.Macros.ND.MultiPoint.ND_MP_GetCount();

                //ND_MP_GetCount実行
                NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(multiPointCount);
                //XY数保持
                culcElement.Add(multiPointCount.Result);

                retInfo.XYCount = multiPointCount.Result;
            }
            ///////////////////////
            // Z
            //Zが有効か調べる
            checkEnable.NDAcquisitionTabName = "Z";
            NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(checkEnable);
            retInfo.enableZ = checkEnable.Result == 1 ? true : false;
            if (checkEnable.Result == 1)
            {

                //Zstep数を取得
                NisMacro.Net.Macro.Macros.ND.ZSeries.ND_GetZSeriesExp zInfo
                = new NisMacro.Net.Macro.Macros.ND.ZSeries.ND_GetZSeriesExp();

                NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(zInfo);
                culcElement.Add(zInfo.ND_LpZSeriesCount);

                retInfo.ZStepCount = zInfo.ND_LpZSeriesCount;
            }

            //全要素の積
            int progress = 0;
            if (culcElement.Count > 0)
            {
                progress = culcElement.Aggregate((d, nextx) => d * nextx);
            }
            retInfo.TotalImagingCount = progress;

            // 使用Ch情報
            var macroImagingChBit = new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ImagingUseImagingChannelBit();
            NisMacro.Net.Execute.MacroQueue.GetInstance.SyncAddMacro(macroImagingChBit);
            retInfo.UseChannelBits = macroImagingChBit.Value;

            return retInfo;
        }
    }
}
