using System;
using System.Collections.Generic;
using System.Reflection;
using NisMacro.Net.Macro.NisInterface;
using System.Text;

namespace NisMacro.Net.Macro.Macros
{
    /// <summary>
    /// マクロ基底クラス
    /// </summary>
    public class NisMacroBase : IMacro
    {
        /// <summary>
        /// マクロ名
        /// </summary>
        public string MacroName
        {
            get;
            set;
        }

        #region IMacroInterface

        public NisMacroBase()
        {
            SetDefaultParamater();
        }

        /// <summary>
        /// デフォルトパラメータを指定するメソッド
        /// 各マクロで実装を行う
        /// </summary>
        internal virtual void SetDefaultParamater()
        {
            return;
        }

        /// <summary>
        /// NisMacro属性を持つメンバを取得する
        /// </summary>
        /// <param name="paramTypes">パラメータ種別</param>
        /// <returns>フィールドメンバ情報</returns>
        List<ExMemberInfo> IMacro.GetNisParamMemberInfo( params eParamType[] paramTypes )
        {
            //  すべてのフィールドメンバを取得してカスタム属性を持つExMemberInfoListをインスタンスする
            List<ExMemberInfo> allMemInfoList = new List<ExMemberInfo>();
            foreach( MemberInfo member in this.GetType().GetFields() )
            {
                allMemInfoList.Add( new ExMemberInfo( member, this ) );
            }            
            
            //  パラメータが一致するもののみ、Return用のリストに追加する
            List<ExMemberInfo> resMemInfoList = new List<ExMemberInfo>();
            foreach( ExMemberInfo exMember in allMemInfoList )
            {
                foreach( eParamType paramType in paramTypes )
                {
                    if( paramType == exMember.ParamType )
                    {
                        resMemInfoList.Add( exMember );
                    }
                }
            }
            
            return resMemInfoList;
        }

        /// <summary>
        /// パラメータをNisMacro属性を持つメンバにセットする
        /// </summary>
        /// <param name="newMemInfoList">メンバ情報リスト</param>
        void IMacro.SetNisParamMemberInfo( List<ExMemberInfo> newMemInfoList )
        {
            //  すべてのフィールドメンバを取得してカスタム属性が一致するメンバへセットする
            List<ExMemberInfo> allMemInfoList = new List<ExMemberInfo>();
            foreach( MemberInfo member in this.GetType().GetFields() )
            {
                allMemInfoList.Add( new ExMemberInfo( member, this ) );
            }

            foreach(FieldInfo member in this.GetType().GetFields() )
            {
                foreach( ExMemberInfo newMember in newMemInfoList )
                {
                    if( member.Name == newMember.Name )
                    {
                        member.SetValue( this, newMember.Value ); 
                    }
                }
            }
        }

        /// <summary>
        /// クラスで定義されているマクロ名を返す
        /// </summary>
        /// <returns>マクロ名</returns>
        string IMacro.GetMacroName()
        {
            return this.MacroName;
        }

        /// <summary>
        /// マクロ実行の準備を行う
        /// ここで共有データに値をセットし、マクロ文字列を作成する
        /// </summary>
        /// <returns>送信用文字列</returns>
        string IMacro.CreateCmd()
        {
            //  パラメータ部分のメンバーを取得して、Index順にソートする
            List<ExMemberInfo> members = ( (IMacro)this ).GetNisParamMemberInfo( eParamType.InParam, eParamType.OutParam );
            members.Sort( ( a, b ) => a.Index - b.Index );

            /*
             * Include部分生成
             */
            StringBuilder cmd = new StringBuilder();
            cmd.AppendFormat( "#include \"{0}\"", NisMacro.Net.Setting.MacroSetting.Instance.MacroIncludeFilePath );
            cmd.AppendLine();
            cmd.AppendLine();

            /*
             * 変数宣言部分生成( intVal_In1;、intval_Out2; 等 )
             */
            List<ExMemberInfo> membersAndResult = ( (IMacro)this ).GetNisParamMemberInfo( eParamType.InParam, eParamType.OutParam, eParamType.Return );
            membersAndResult.Sort( ( a, b ) => a.Index - b.Index );
            foreach( ExMemberInfo exMem in membersAndResult )
            {
                if( exMem.ValueType == typeof( sbyte ) )
                {
                    cmd.AppendFormat("char {0};", exMem.GetMemberName);
                    cmd.AppendLine();
                }
                if( exMem.ValueType == typeof( byte ) )
                {
                    cmd.AppendFormat( "byte {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( short ) )
                {
                    cmd.AppendFormat( "short {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( ushort ) )
                {
                    cmd.AppendFormat( "word {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( int ) )
                {
                    cmd.AppendFormat( "int {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( uint ) )
                {
                    cmd.AppendFormat( "dword {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( double ) )
                {
                    cmd.AppendFormat( "double {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( long ) )
                {
                    cmd.AppendFormat( "long {0};", exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( string ) )
                {
                    cmd.AppendFormat( "wchar_t {0}[{1}];", exMem.GetMemberName,　Define.STR_BUFFER_LENGTH );
                    cmd.AppendLine();
                }
                //if( exMem.ValueType == typeof( int[] ) )
                //{
                //    cmd += string.Format( "int[] {0}[1024];", exMem.GetMemberName );
                //    cmd += System.Environment.NewLine;
                //}
                //if( exMem.ValueType == typeof( double[] ) )
                //{
                //    cmd += string.Format( "double[] {0}[1024];", exMem.GetMemberName );
                //    cmd += System.Environment.NewLine;
                //}
            }

            cmd.AppendLine();

            /*
             * Get***部分生成( intVal_In1 = GetIntVal(1) 等 )
             */
            foreach( ExMemberInfo exMem in members )
            {
                if( exMem.ParamType == eParamType.InParam )
                {
                    if( exMem.ValueType == typeof( sbyte ) )
                    {
                        cmd.AppendFormat( "{0} = GetCharVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( byte ) )
                    {
                        cmd.AppendFormat( "{0} = GetByteVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( short ) )
                    {
                        cmd.AppendFormat( "{0} = GetShortVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( ushort ) )
                    {
                        cmd.AppendFormat( "{0} = GetWordVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( int ) )
                    {
                        cmd.AppendFormat( "{0} = GetIntVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( uint ) )
                    {
                        cmd.AppendFormat( "{0} = GetUintVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( double ) )
                    {
                        cmd.AppendFormat( "{0} = GetDoubleVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( long ) )
                    {
                        cmd.AppendFormat( "{0} = GetLongVal({1});", exMem.GetMemberName, exMem.Index );
                        cmd.AppendLine();
                    }
                    if ( exMem.ValueType == typeof( string ) )
                    {
                        cmd.AppendFormat( "GetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName );
                        cmd.AppendLine();
                    }
                    if (exMem.ValueType == typeof(string))
                    {
                        cmd.AppendFormat("GetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName);
                        cmd.AppendLine();
                    }
                    //if( exMem.ValueType == typeof( int[] ) )
                    //{
                    //    cmd += ( string.Format( "GetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName ) );
                    //    cmd += System.Environment.NewLine;
                    //}
                    //if( exMem.ValueType == typeof( double[] ) )
                    //{
                    //    cmd += ( string.Format( "GetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName ) );
                    //    cmd += System.Environment.NewLine;
                    //}
                }
            }

            cmd.AppendLine();

            /*
             * 関数Call部分生成 ( "マクロ名(引数1, 引数2, ...)")
             */
            List<ExMemberInfo> resMember = ( (IMacro)this ).GetNisParamMemberInfo( eParamType.Return );
            if( 0 != resMember.Count )
            {
                cmd.AppendFormat( "{0} = ", resMember[0].GetMemberName );
            }
            List<string> memberNameList = new List<string>();   //  変数名一覧リスト
            foreach( ExMemberInfo exMem in members )
            {
                //  ポインタ渡しをする場合は「&」をつける
                if( true == exMem.IsPointer )
                {
                    memberNameList.Add( string.Format( "{0}{1}", "&", exMem.GetMemberName ) );
                }
                else
                {
                    memberNameList.Add( exMem.GetMemberName );
                }
            }
            cmd.AppendFormat( "{0}({1});", ( (IMacro)this ).GetMacroName(), string.Join( ", ", memberNameList ) ) ;

            cmd.AppendLine();
            cmd.AppendLine();

            /*
             * 結果取得部分生成
             */
            List<ExMemberInfo> outMembersAndResult = ( (IMacro)this ).GetNisParamMemberInfo( eParamType.Return, eParamType.OutParam );
            foreach( ExMemberInfo exMem in outMembersAndResult )
            {
                if( exMem.ValueType == typeof( sbyte ) )
                {
                    cmd.AppendFormat( "SetCharVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( byte ) )
                {
                    cmd.AppendFormat( "SetByteVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( short ) )
                {
                    cmd.AppendFormat( "SetShortVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( ushort ) )
                {
                    cmd.AppendFormat( "SetWordVal({0}, {1});", exMem.Index, exMem.GetMemberName  );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( int ) )
                {
                    cmd.AppendFormat( "SetIntVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( uint ) )
                {
                    cmd.AppendFormat( "SetUintVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( double ) )
                {
                    cmd.AppendFormat( "SetDoubleVal({0}, {1});", exMem.Index, exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( long ) )
                {
                    cmd.AppendFormat( "SetLongVal({0}, {1});", exMem.Index, exMem.GetMemberName ) ;
                    cmd.AppendLine();
                }
                if ( exMem.ValueType == typeof( string ) )
                {
                    cmd.AppendFormat( "SetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName );
                    cmd.AppendLine();
                }
                if (exMem.ValueType == typeof(string))
                {
                    cmd.AppendFormat("SetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName);
                    cmd.AppendLine();
                }
                //if( exMem.ValueType == typeof( int[] ) )
                //{
                //    cmd += ( string.Format( "SetStringVal({0}, {1});", exMem.Index, exMem.GetMemberName ) );
                //    cmd += System.Environment.NewLine;
                //}
                //if( exMem.ValueType == typeof( double[] ) )
                //{
                //    cmd += ( string.Format( "SetDoubleArrayVal({0}, {1});", exMem.Index, exMem.GetMemberName ) );
                //    cmd += System.Environment.NewLine;
                //}
            }

            cmd.AppendLine();

            return cmd.ToString();
        }

        #endregion



        
    }
}
