using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NisMacro.Net.Macro.NisInterface;

namespace NisMacro.Net.Macro.Variable
{
    /// <summary>
    /// Nis上の変数アクセスクラス定義
    /// </summary>
    public class NisVariableBase : IVariable
    {
        /// <summary>
        /// 変数タイプ
        /// </summary>
        public Type VarType
        {
            get;
            private set;
        }

        /// <summary>
        /// 変数名
        /// </summary>
        public string VarName
        {
            get;
            private set;
        }

        /// <summary>
        /// グローバル変数かどうか
        /// </summary>
        public bool IsGlobal
        {
            get;
            private set;
        }

        /// <summary>
        /// 変数タイプを取得
        /// </summary>
        Type IVariable.GetVariableType()
        {
            return this.VarType;
        }

        /// <summary>
        /// NisからGlobal変数を取得する
        /// </summary>
        /// <returns></returns>
        object IVariable.GetValue()
        {
            //  データリスト初期化
            StoredData.GetInstance.ClearDataList();

            //  データ追加(取得用に空のデータ構造を作成しておく)
            StoredData.GetInstance.AddData( VarType, null );

            //  Nisから変数値取得
            return CommandManager.GetInstance.GetVariable( this );
        }

        void IVariable.SetValue(object val)
        {
            //  :ToDo    Nisへ値を更新するコマンド実装
        }

        /// <summary>
        /// コンストラクタ1
        /// </summary>
        /// <param name="type">種別</param>
        /// <param name="name">変数名</param>
        /// <param name="name"></param>
        public NisVariableBase( Type type, string name )
        {
            this.VarType = type;
            this.VarName = name;
            this.IsGlobal = true;
        }

        /// <summary>
        /// コンストラクタ2
        /// </summary>
        /// <param name="type">種別</param>
        /// <param name="name">変数名</param>
        /// <param name="defaultVal">デフォルト値</param>
        /// <param name="isGlobal">グローバルかどうか</param>
        public NisVariableBase( Type type, string name, object defaultVal, bool isGlobal = true )
        {
            this.VarType = type;
            this.VarName = name;
            this.IsGlobal = isGlobal;
            
            //  Nis上にインスタンスする
            CommandManager.GetInstance.SetVariable( this, defaultVal );
        }

        /// <summary>
        /// NISにインスタンスする用の文字列を作成する
        /// </summary>
        /// <returns></returns>
        string IVariable.CreateSetCmd()
        {
            /*
             * Include部分生成
             */
            string cmd = string.Empty;
            cmd += string.Format( "#include \"{0}\"", NisMacro.Net.Setting.MacroSetting.Instance.MacroIncludeFilePath);
            cmd += System.Environment.NewLine;
            cmd += System.Environment.NewLine;

            /*
             * グローバル宣言
             */
            if( true == this.IsGlobal )
            {
                cmd += "global ";
            }

            /*
             * 変数宣言部分生成
             */
            int dataIndex = 0;  //  変数のときのIndexは0固定とする
            if( this.VarType == typeof( sbyte ) )
            {
                cmd += string.Format( "char {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetCharVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( byte ) )
            {
                cmd += string.Format( "byte {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetByteVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( short ) )
            {
                cmd += string.Format( "short {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetShortVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( ushort ) )
            {
                cmd += string.Format( "word {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetWordVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( int ) )
            {
                cmd += string.Format( "int {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetIntVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( uint ) )
            {
                cmd += string.Format( "dword {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetUintVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( double ) )
            {
                cmd += string.Format( "double {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetDoubleVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( long ) )
            {
                cmd += string.Format( "long {0};", this.VarName );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "{0} = GetLongVal({1});", this.VarName, dataIndex ) );
            }
            if( this.VarType == typeof( string ) )
            {
                cmd += string.Format( "wchar_t {0}[{1}];", this.VarName, Define.STR_BUFFER_LENGTH );
                cmd += System.Environment.NewLine;
                cmd += ( string.Format( "GetStringVal({0}, {1});", dataIndex, this.VarName ) );
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

            cmd += System.Environment.NewLine;
            cmd += System.Environment.NewLine;

            return cmd;
        }

        /// <summary>
        /// Nisから取得するコマンド文字列を作成する
        /// </summary>
        /// <returns></returns>
        string IVariable.CreateGetCmd()
        {
            /*
             * Include部分生成
             */
            int dataIndex = 0;  //  変数のときのIndexは0固定とする
            string cmd = string.Empty;
            cmd += string.Format( "#include \"{0}\"", NisMacro.Net.Setting.MacroSetting.Instance.MacroIncludeFilePath);
            cmd += System.Environment.NewLine;
            cmd += System.Environment.NewLine;

            if( this.VarType == typeof( sbyte ) )
            {
                cmd += ( string.Format( "SetCharVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( byte ) )
            {
                cmd += ( string.Format( "SetByteVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( short ) )
            {
                cmd += ( string.Format( "SetShortVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( ushort ) )
            {
                cmd += ( string.Format( "SetWordVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( int ) )
            {
                cmd += ( string.Format( "SetIntVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( uint ) )
            {
                cmd += ( string.Format( "SetUintVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( double ) )
            {
                cmd += ( string.Format( "SetDoubleVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( long ) )
            {
                cmd += ( string.Format( "SetLongVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
            }
            if( this.VarType == typeof( string ) )
            {
                cmd += ( string.Format( "SetStringVal({0}, {1});", dataIndex, this.VarName ) );
                cmd += System.Environment.NewLine;
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

            return cmd;
        }

        /// <summary>
        /// Nis上のグローバル変数を取得
        /// </summary>
        /// <param name="type">型</param>
        /// <param name="name">変数名</param>
        /// <returns>変数値(見つからない場合、nullを返す)</returns>
        public object GetNisVariable( Type type, string name )
        {
            try
            {
                //  データリスト初期化
                StoredData.GetInstance.ClearDataList();

                //  データ追加(取得用に空のデータ構造を作成しておく)
                StoredData.GetInstance.AddData( type, null );

                //  コマンド作成
                string cmd = ( (IVariable)this ).CreateGetCmd();

                /*
                 *  変数取得要求を同期で実行
                 */
                NisMacro.Net.Interprocess.Command.GetInstance().Execute( cmd );

                int dataIndex = 0;  //  変数のときのIndexは0固定とする
                return StoredData.GetInstance.GetData( dataIndex, type );
            }
            catch
            {
                return null;
            }
        }    
    }
}
