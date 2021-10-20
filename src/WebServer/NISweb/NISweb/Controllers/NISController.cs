using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using NISweb.Common;
using NISweb.Models;
using NISweb.Models.FileUtil;
using NISweb.Models.HttpUtil;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace NISweb.Controllers
{
    public class NISController : Controller
    {
        /// <summary>
        /// 初期化クラス
        /// </summary>
        private InitialSetting init;
        /// <summary>
        /// ファイル操作クラス
        /// </summary>
        private FileOperator fileOperator;
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        private string ErrorMessageStr = string.Empty;

        /// <summary>
        /// Web.Configの読み込み
        /// </summary>
        public NISController()
        {
            init = InitialSetting.GetInstance();
            fileOperator = FileOperator.GetInstance();
            ErrorMessageStr = string.Empty;
            init.DeleteMessage = string.Empty;
        }

        public ActionResult Main()
        {
            return View();
        }

        //[ChildActionOnly]
        public ActionResult MainContent(string Connect, string IsInExperiment, string StageLocation)
        {
            Connect = "1";
            IsInExperiment = "1";
            StageLocation = "1";

            //HTTPconnect生存確認
            bool isAliveServer = false;

            HttpMessageController httpMessageControl = new HttpMessageController();

            //接続確認
            isAliveServer = httpMessageControl.ResponseToString(
                httpMessageControl.Polling(init.ServerIP,
                init.ServerPort,
                init.TimeOut,
                CommonDefine.ND_IsInExperimentCapture),
                ref init.RunAcquisitionFlg);

            if (!string.IsNullOrEmpty(Connect) || !string.IsNullOrEmpty(IsInExperiment))
            {
                //ND_IsInExperimentCapture確認
                isAliveServer = httpMessageControl.ResponseToString(
                    httpMessageControl.Polling(init.ServerIP,
                    init.ServerPort,
                    init.TimeOut,
                    CommonDefine.ND_IsInExperimentCapture),
                    ref init.RunAcquisitionFlg);
            }
            if (!string.IsNullOrEmpty(StageLocation))
            {
                isAliveServer = httpMessageControl.ResponseToString(
                httpMessageControl.Polling(init.ServerIP,
                init.ServerPort,
                init.TimeOut,
                CommonDefine.StgGetPosXY),
                ref init.XYstg);

                isAliveServer = httpMessageControl.ResponseToString(
                    httpMessageControl.Polling(init.ServerIP,
                    init.ServerPort,
                    init.TimeOut,
                    CommonDefine.StgGetPosZ),
                    ref init.Zstg);
            }

            if (!isAliveServer)
            {
                //画面出力要素
                ViewBag.ConnectionStatus = "DisConnected";
                ViewBag.RunAcquisition = "-";
                ViewBag.Xstg = "-";
                ViewBag.Ystg = "-";
                ViewBag.Zstg = "-";
                return PartialView("_MainContentView");
            }


            //画面出力要素
            ViewBag.ConnectionStatus = "Connected";

            ViewBag.RunAcquisition = int.Parse(init.RunAcquisitionFlg) == 1 ? "Run" : "Stop";

            var xyStgArray = init.XYstg.Split(',');
            int index = 0;
            ViewBag.Xstg = xyStgArray[index];
            ViewBag.Ystg = xyStgArray[++index];
            ViewBag.Zstg = init.Zstg;

            return PartialView("_MainContentView");
        }

        public ActionResult ApiLog()
        {
            return View();
        }

        public ActionResult APILogContent(string ReLoad)
        {
            //Log更新のため、サーバ側のLogは削除
            init.DeleteFiles(init.ServerLogPath);
            ViewBag.DeleteMessage = init.DeleteMessage;

            //Log取得
            ViewBag.ErrorMessageStr = fileOperator.GetLogFile(init.LogPath, init.ServerLogPath, ref ErrorMessageStr);

            ViewBag.APIDate = fileOperator.RunTime;
            ViewBag.ClientIP = fileOperator.Client;
            ViewBag.APIName = fileOperator.Instruction;
            ViewBag.Status = fileOperator.Status;

            return PartialView("_APILogContentView");
        }

        public ActionResult ImageList(string download, string ReLoad)
        {
            if (download != null)
            {
                //ダウンロードボタン押下の度にフォルダクリア
                init.DeleteFiles(init.ServerImagePath);
                ViewBag.DeleteMessage = init.DeleteMessage;

                string downloadfile = fileOperator.DownLoadFile(download, init.ServerImagePath, ref ErrorMessageStr);
                ViewBag.ErrorMessageStr = ErrorMessageStr;
                string defoultFileName = fileOperator.ProcessingPath(download, @"\", true, string.Empty);
                return File(downloadfile, "image/tiff", defoultFileName);
            }

            //tifファイル全てを取得
            fileOperator.TransportFiles(init.LocalImagePath);

            ViewBag.CreationTime = fileOperator.FileCreationDate;
            ViewBag.Length = fileOperator.FileLength;
            ViewBag.FullPath = fileOperator.FileFullPath;
            //監視対象dir
            ViewBag.ImageDir = init.LocalImagePath;

            //ボタン押下時に追加される
            ViewBag.Link = fileOperator.FileLink;

            return View();
        }


        public ActionResult RetrievedImage()
        {
            return View();
        }

        public ActionResult RetrievedImageContent(string ReLoad)
        {
            //前回の表示対象CurrentImageを削除(最新画像は更新されていくため、Log等とは分ける)
            init.DeleteFiles(init.ServerCurrentImagePath);
            ViewBag.DeleteMessage = init.DeleteMessage;

            //最新画像ファイル名取得
            string[] imageNames = fileOperator.GetCurrentImageName(init.LocalImagePath, init.ServerCurrentImagePath, ref ErrorMessageStr);
            ViewBag.ErrorMessageStr = ErrorMessageStr;

            ViewBag.CurrentImageName = fileOperator.GetImageFileName(imageNames);

            //最新画像ファイルパス取得
            ViewBag.CurrentImage = fileOperator.GetCurrentImage(imageNames, init.ServerCurrentImagePath, init.ServerCurrentContentPath);

            return PartialView("_RetrievedImageContentView");
        }

        public ActionResult Exception()
        {
            throw new System.Exception("");
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}