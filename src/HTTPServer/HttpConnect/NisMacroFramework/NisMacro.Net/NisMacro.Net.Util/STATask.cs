using System;
using System.Threading;
using System.Threading.Tasks;

namespace NisMacro.Net.Util
{
    /// <summary>
    /// STAで実行されるTaskオブジェクトを生成する
    /// </summary>
    public class STATask
    {
        /// <summary>
        /// STAのスレッドを開始します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func">実行するメソッド</param>
        /// <returns></returns>
        public static Task Run<T>(Func<T> func)
        {
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);       // STAを設定
            thread.Start();     //スレッド開始
            return tcs.Task;    // Taskを返す
        }

        /// <summary>
        /// STAのスレッドを開始します
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public static Task Run(Action act)
        {
            return Run(() =>
            {
                act();
                return true;
            });
        }
    }
}
