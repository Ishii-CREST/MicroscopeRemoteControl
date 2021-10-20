using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Info
{
    public class GetImagingInfo:NisAPIBase
    {
        protected override bool RunAPIImpl()
        {
            try
            {

                // 幅、高さ,Z,T,Chを取得
                // 幅を取得
                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ImagingScannerWidth widthMacro =
                new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ImagingScannerWidth();
                AddMacroToQueSync(widthMacro);

                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ImagingScannerHeight heightMacro =
                    new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ImagingScannerHeight();
                AddMacroToQueSync(heightMacro);

                NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ScanAreaAspect aspMacro =
                    new NisMacro.Net.Macro.Macros.Acquire.Camera.Properties.CameraGet_ScanAreaAspect();
                AddMacroToQueSync(aspMacro);


                // ND情報を取得
                var ndInfo = NisAPIUtil.GetNDExpInfo();

                int chCount = 0;
                for (int i = 0; i < 32; i++)
                {
                    if ((ndInfo.UseChannelBits >> i & 1) == 1) chCount++;
                }

                // 正方形(Aspect=1)の場合はheightはWidthと同じ
                int height = heightMacro.Value;
                if(aspMacro.Value == 1)
                {
                    height = widthMacro.Value;
                }

                // 戻りに詰める
                HttpResponse.ResponseParam.Add(widthMacro.Value);
                HttpResponse.ResponseParam.Add(height);
                HttpResponse.ResponseParam.Add(ndInfo.loopT.Sum());
                HttpResponse.ResponseParam.Add(ndInfo.XYCount);
                HttpResponse.ResponseParam.Add(ndInfo.ZStepCount);
                HttpResponse.ResponseParam.Add(chCount);

                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.OK;

                return true;
            }
            catch(Exception ex)
            {
                HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.InternalServerError;
                return false;
            }
        }
    }
}
