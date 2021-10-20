using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Images
{
    public class ND_RunSequentialStimulationExp : NisAPIBase
    {
        private enum eQueryIndex
        {
            prefix = 0,
        }
        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.prefix, new StringValue() { isOption = true });
            CreateParamList(typeof(eQueryIndex));
        }

        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                FileInfo f = FilePathCreator.CreateCurrentDateTimeFilePath(ParamValues[eQueryIndex.prefix].ToStr,
                    "nd2",
                     out DateTime createDate);


                //マクロ実行前の準備

                NisMacro.Net.Macro.Macros.ConfocalA1.A1ApplyStimulationSettings setting
                    = new NisMacro.Net.Macro.Macros.ConfocalA1.A1ApplyStimulationSettings();

                AddMacroToQueSync(setting);

                NisMacro.Net.Macro.Macros.ND.Stimulation.ND_RunSequentialStimulationExp macro
                    = new NisMacro.Net.Macro.Macros.ND.Stimulation.ND_RunSequentialStimulationExp();

                //マクロ実行
                AddMacroToQueSync(macro);
                
                //nd2ファイル作成
                NisMacro.Net.Macro.Macros.ImageDocument.Save.ImageSaveAs mImgSave
                                = new NisMacro.Net.Macro.Macros.ImageDocument.Save.ImageSaveAs()
                                {
                                    Image = f.FullName,
                                    ImType = 15,
                                    ImCompr = 0
                                };
                //nd2保存
                AddMacroToQueSync(mImgSave);

                string prefix = ParamValues[eQueryIndex.prefix].ToStr;
                if (string.IsNullOrEmpty(prefix))
                {
                    prefix = "\\";      // \ を入れるとprefixがなしになる。
                }
                //nd2⇒tif変換
                NisMacro.Net.Macro.Macros.ND.Experiment.ND_ExportToTIFF toTiff
                    = new NisMacro.Net.Macro.Macros.ND.Experiment.ND_ExportToTIFF()
                    {
                        //Filenameはnd2ファイルのファイル名を指定する
                        Filename = f.FullName,
                        Dest_Directory = f.DirectoryName,
                        Prefix = prefix,
                        ChannelExportType = 0,
                        ApplyLuts = 0,
                        uiSaveAsMCH = 0,
                        iMultiPageMask = 0
                    };

                //tifファイル保存
                AddMacroToQueSync(toTiff);

                // 20200519 
                // ND2ファイル削除
                f.Delete();

                // HTTPレスポンスデータ作成
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
