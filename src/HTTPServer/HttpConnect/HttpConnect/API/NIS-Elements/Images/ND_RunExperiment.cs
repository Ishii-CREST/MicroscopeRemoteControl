using HttpConnect.API.APIValue;
using HttpConnect.HttpUtil.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.API.NIS_Elements.Images
{
    public class ND_RunExperiment : NisAPIBase
    {
        public enum eQueryIndex
        {
            IsSync = 0,
            enableT,
            enableXY,
            enableZ,
            Prefix,
            
        }

        protected override void DefineAPIParams()
        {
            ParamValues.Add(eQueryIndex.IsSync, new IntValue());
            ParamValues.Add(eQueryIndex.enableT, new IntValue());
            ParamValues.Add(eQueryIndex.enableXY, new IntValue());
            ParamValues.Add(eQueryIndex.enableZ, new IntValue());
            ParamValues.Add(eQueryIndex.Prefix, new StringValue() { isOption = true });
            CreateParamList(typeof(eQueryIndex));
        }


        #region method
        protected override bool RunAPIImpl()
        {
            try
            {
                //ファイル名作成指示
                //APIElements.SetFileName();
                //出力先パス作成
                // UserUtil.FileController.CreateFileFullPath();
                string dir = FilePathCreator.CreateCurrentDateTimeDir(out DateTime dt).FullName;

                //saveにチェック
                NisMacro.Net.Macro.Macros.ND.NDEnableSaveToTIFF saveToTiff
                    = new NisMacro.Net.Macro.Macros.ND.NDEnableSaveToTIFF()
                    {
                        ND_EnableSaveToTIFF = 1
                    };

                // フォルダ名指定されていない場合は、日付フォルダを作成


                //前提設定
                NisMacro.Net.Macro.Macros.ND.Experiment.ND_DefineExperiment define
                    = new NisMacro.Net.Macro.Macros.ND.Experiment.ND_DefineExperiment()
                    {
                        //time
                        ND_TExp = ParamValues[eQueryIndex.enableT].ToInt,
                        //xy
                        ND_XYExp = ParamValues[eQueryIndex.enableXY].ToInt,
                        //zstac
                        ND_ZExp = ParamValues[eQueryIndex.enableZ].ToInt,
                        //lambda
                        ND_LExp = 0,
                        //sort
                        ND_NormalZOrder = 0,
                        //dir
                        Filename = dir + "\\",
                        //new folder
                        Prefix = ParamValues[eQueryIndex.Prefix].ToStr,
                        //save
                        ND_FileType = 1,
                        //output tiff
                        ND_UseTIFF = 1,
                        // UsePFS
                        ND_UsePFS = 0,
                        // Measre
                        ND_EnableTMeas = 0
                    };


                NisMacro.Net.Macro.Macros.ND.Experiment.ND_RunExperiment runExp
                    = new NisMacro.Net.Macro.Macros.ND.Experiment.ND_RunExperiment()
                    {
                        EventDescription = 0
                    };


                //マクロ実行
                if (ParamValues[eQueryIndex.IsSync].ToInt == 0)
                {
                    AddMacroToQueAsync(saveToTiff);
                    AddMacroToQueAsync(define);
                    // 実行情報を保存
                    InternalData.NIS_Elements.NisInternalData.Instance.LastNDExperimentInfo = NisAPIUtil.GetNDExpInfo();
                    AddMacroToQueAsync(runExp);
                }
                else
                {
                    AddMacroToQueSync(saveToTiff);
                    AddMacroToQueSync(define);
                    // 実行情報を保存
                    InternalData.NIS_Elements.NisInternalData.Instance.LastNDExperimentInfo = NisAPIUtil.GetNDExpInfo();
                    AddMacroToQueSync(runExp);
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();
                while(!InternalData.NIS_Elements.NisInternalData.Instance.IsRunningNDExpriment )
                {
                    if(sw.Elapsed.TotalSeconds > 60) // 最大60sec待機
                    {
                        // タイムアウト
                        HttpResponse.ResultStatusCode = CommonDefine.eHttpStatusCode.RequestTimeOut;
                        return true;
                    }
                    // NDExpが開始されるまで待機する。
                    //  →開始してからレスポンスを返す。
                    System.Threading.Thread.Sleep(1);
                }
                sw.Stop();
               
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
