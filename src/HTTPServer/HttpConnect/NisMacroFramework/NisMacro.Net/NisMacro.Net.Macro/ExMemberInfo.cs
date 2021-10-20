using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NisMacro.Net.Macro;
using NisMacro.Net.Macro.NisInterface;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// Refrection.MemberInfoの拡張
    /// マクロパラメータ属性を付加
    /// </summary>
    public class ExMemberInfo
    {
        /// <summary>
        /// メンバー情報
        /// </summary>
        private MemberInfo _memberInfo;

        /// <summary>
        /// マクロパラメータ属性
        /// </summary>
        public NisMacroAttribute MacroFieldAttribute
        {
            get;
            private set;
        }

        /// <summary>
        /// インデックス(属性)
        /// </summary>
        public int Index
        {
            get
            {
                return this.MacroFieldAttribute.Index;
            }
        }

        /// <summary>
        /// ポインタで取得するかどうか
        /// </summary>
        public bool IsPointer
        {
            get
            {
                return this.MacroFieldAttribute.IsPointer;
            }
        }

        /// <summary>
        /// 属性タイプ(System.Int32、System.stringとか)
        /// </summary>
        public Type ValueType
        {
            get
            {
                return ( (System.Reflection.FieldInfo)this._memberInfo ).FieldType;
            }
        }

        /// <summary>
        /// リストで取得する場合のサイズ。
        /// </summary>
        public int ArraySize { get { return this.MacroFieldAttribute.ArraySize; } }


        /// <summary>
        /// パラメータタイプ(属性)
        /// </summary>
        public eParamType ParamType
        {
            get
            {
                return this.MacroFieldAttribute.ParamType;
            }
        }

        /// <summary>
        /// パラメータ名
        /// </summary>
        public string Name
        {
            get
            {
                return this._memberInfo.Name;
            }
        }

        /// <summary>
        /// パラメータの値
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// NisElementsとの連携時に作成する文字列内の変数名を返す
        /// </summary>
        public string GetMemberName
        {
            get
            {
                string str = string.Format( "{0}_{1}", this.ParamType.ToString(), this.Name );
                return str;
            }
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="memberInfo">メンバー情報</param>
        public ExMemberInfo( MemberInfo memberInfo, IMacro iMacro )
        {
            //  privateフィールドに値をセット
            this._memberInfo = memberInfo;
            this.Value = ( (System.Reflection.FieldInfo)this._memberInfo ).GetValue( iMacro );

            this.MacroFieldAttribute = (NisMacroAttribute)_memberInfo.GetCustomAttribute( typeof( NisMacroAttribute ) );
        }
    }
}
