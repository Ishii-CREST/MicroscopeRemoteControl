using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HttpConnect
{
    public static class Program
    {
        /// <summary>
        /// 一度起動したかどうか
        /// </summary>
        static bool IsSecondExecute = false;
        /// <summary>
        /// ２重起動防止Mutex名
        /// </summary>
        private const string MUTEX_NAME = "_HTTP_CONNECT_";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                System.Threading.Mutex m;
                // Mutexが開けたら、既に実行中なので、2重起動はさせない。
                if (System.Threading.Mutex.TryOpenExisting(MUTEX_NAME, out m))
                {
                    MessageBox.Show("multiple run is not available.");
                    return;
                }

                // 管理者権限を持っていないとHTTPサーバを登録できない
                if (false == IsAdministrator())
                {
                    MessageBox.Show("This application requires administrator privileges","Running Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (false == IsSecondExecute)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    IsSecondExecute = true;
                }
                //設定ファイル読込
                RequestRelated.Instance.SetRelated();

                // ログインスタンスの生成
                InternalData.CommonInternal.Instance.APILogManage = new LogManage(RequestRelated.Instance.OutnPutAPILogPath);
                InternalData.CommonInternal.Instance.AppLogManage = new LogManage(RequestRelated.Instance.OutnPutAppLogPath);

                //過去APILog削除
                InternalData.CommonInternal.Instance.APILogManage.DeleteLog();
                InternalData.CommonInternal.Instance.AppLogManage.DeleteLog(RequestRelated.Instance.MaxAppLogNum);

                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(" ####################################### ");
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(" 　S T A R T     H T T P C o n n e c t   ");
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format(" 　DateTime:{0}", DateTime.Now.ToString(CommonDefine.DATETIME_FORMAT)));
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(string.Format(" 　Version:{0}", UserUtil.UtilMethods.GetVersionStr()));
                InternalData.CommonInternal.Instance.AppLogManage.OutputLog(" ####################################### ");
                NisMacro.Net.Execute.NisExecuteDummy dm = NisMacro.Net.Execute.NisExecuteDummy.Instance;
                // NISから起動しているかチェック
                CheckOnNis();

                dm.StartWaitMacro();

                Forms.HTTPserver mainWindow = new Forms.HTTPserver();
                dm.SetMainWindow(mainWindow);
                dm.ShowMainWindow();
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
        /// <summary>
        /// 現在アプリケーションを実行しているユーザーに管理者権限があるか調べる
        /// </summary>
        /// <returns>管理者権限がある場合はtrue。</returns>
        private static bool IsAdministrator()
        {
            //現在のユーザーを表すWindowsIdentityオブジェクトを取得する
            System.Security.Principal.WindowsIdentity wi =
                System.Security.Principal.WindowsIdentity.GetCurrent();
            //WindowsPrincipalオブジェクトを作成する
            System.Security.Principal.WindowsPrincipal wp =
                new System.Security.Principal.WindowsPrincipal(wi);
            //Administratorsグループに属しているか調べる
            return wp.IsInRole(
                System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// NISから起動されているかを確認する
        /// </summary>
        private static void CheckOnNis()
        {
#if !DEBUG
            if (!NisMacro.Net.Util.NisArMonitor.Instance.IsNisElementsAlive)
            {
                throw new Exception("Please run from NIS-Elements.");
            }
#endif
        }
    }
}
