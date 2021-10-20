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
    public class Capture : NisAPIBase
    {
        private enum eQueryIndex
        {
            prefix = 0,
        }


        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.prefix,new StringValue(){isOption = true});
            CreateParamList(typeof(eQueryIndex));
        }


        #region method
		protected override bool RunAPIImpl()
        {
            try
            {
                //出力先パス作成
                FileInfo createFile = FilePathCreator.CreateCurrentDateTimeFilePath(
                    string.Empty
                    , "nd2",
                    out DateTime createDate);

                NisMacro.Net.Macro.Macros.Acquire.Capture.Capture macro =
                    new NisMacro.Net.Macro.Macros.Acquire.Capture.Capture();

                //Capture実行
                AddMacroToQueSync(macro);
                
                //nd2ファイル作成
                NisMacro.Net.Macro.Macros.ImageDocument.Save.ImageSaveAs mImgSave
                                = new NisMacro.Net.Macro.Macros.ImageDocument.Save.ImageSaveAs()
                                {
                                    Image = createFile.FullName,
                                    ImType = 15,
                                    ImCompr = 0
                                };
                
                //nd2保存
                AddMacroToQueSync(mImgSave);

                string prefix = ParamValues[eQueryIndex.prefix].ToStr;
                if (string.IsNullOrEmpty(prefix))
                {
                    prefix = "\\";      // \ を入れるとprefix=なしの扱い
                }
                //nd2⇒tif変換
                NisMacro.Net.Macro.Macros.ND.Experiment.ND_ExportToTIFF toTiff
                    = new NisMacro.Net.Macro.Macros.ND.Experiment.ND_ExportToTIFF()
                    {
                        //Filenameはnd2ファイルのファイル名を指定する
                        Filename = createFile.FullName ,
                        Dest_Directory = createFile.DirectoryName + "\\",
                        Prefix =  prefix,
                        ChannelExportType = 0,
                        ApplyLuts = 0,
                        uiSaveAsMCH = 0,
                        iMultiPageMask = 0
                    };

                //tifファイル保存
                AddMacroToQueSync(toTiff);

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
