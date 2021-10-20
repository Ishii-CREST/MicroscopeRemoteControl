using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NisMacro.Net.Macro;

namespace NisMacro.Net.Data
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
        /// マクロインターフェース
        /// </summary>
        private IMacro _iMacro;

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
            get
            {
                return ( (System.Reflection.FieldInfo)this._memberInfo ).GetValue( this._iMacro );
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
            this._iMacro = iMacro;

            this.MacroFieldAttribute = (NisMacroAttribute)_memberInfo.GetCustomAttribute( typeof( NisMacroAttribute ) );
        }
    }
}
