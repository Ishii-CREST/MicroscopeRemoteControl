using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NisMacro.Net.Macro.NisInterface;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// マクロ関係のユーティリティ静的クラス
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Imacroを継承したTypeのListを返す
        /// </summary>
        /// <returns>IMacroを継承したクラス</returns>
        public static List<Type> GetINisMacroTypes()
        {
            List<Type> iMacroTypeList = new List<Type>();

            foreach( Type tCurClass in Assembly.GetExecutingAssembly().GetTypes() )
            {
                foreach( Type tInterface in tCurClass.GetInterfaces() )
                {
                    if( tInterface == typeof( IMacro ) )
                    {
                        //  MacroBaseを継承したものだけ追加する
                        if( tCurClass.BaseType == typeof( Macro.Macros.NisMacroBase ) )
                        {
                            iMacroTypeList.Add( tCurClass );
                        }
                    }
                }
            }

            return iMacroTypeList;
        }

        /// <summary>
        /// クラスの妥当性チェックメソッド
        /// NisElementsに渡したときにエラーが発生するような条件にならないようにチェックするためのメソッド
        /// </summary>
        /// <param name="iMacro">インターフェース</param>
        /// <returns></returns>
        public static bool CheckInvalid( IMacro iMacro )
        {
            try
            {
                /*
                 * MacroName 定義チェック
                 */
                if( true == string.IsNullOrEmpty( iMacro.GetMacroName() ) )
                {
                    throw new Exception( "Exception of undefined macro name." );
                }

                /*
                 * Result パラメータチェック
                 */
                List<ExMemberInfo> resultMem = iMacro.GetNisParamMemberInfo( eParamType.Return, eParamType.Return_Ignore );
                //  Return属性のメンバを取得し、複数ある場合は例外を発生させる
                if( 1 != resultMem.Count())
                {
                    throw new Exception( "Exception of no or duplicate result Paramater. result paramater have to one object." );
                }

                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
