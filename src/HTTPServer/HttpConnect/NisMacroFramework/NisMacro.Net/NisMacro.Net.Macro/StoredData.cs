using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NisMacro.Net.Macro
{
    /// <summary>
    /// NisElementsとの値連携用データ
    /// </summary>
    public class StoredData
    {
        #region シングルトン宣言

        /// <summary>
        /// シングルトン実態
        /// </summary>
        private static StoredData _getSetData;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private StoredData()
        {
        }

        /// <summary>
        /// 唯一のインスタンス取得
        /// </summary>
        public static StoredData GetInstance
        {
            get
            {
                if( null == _getSetData )
                {
                    _getSetData = new StoredData();
                }

                return _getSetData;
            }
        }

        #endregion

        /// <summary>
        /// データリスト
        /// </summary>
        private List<object> _dataList = new List<object>();

        /// <summary>
        /// パラメータデータを追加(InterProcessから使用する)
        /// </summary>
        /// <param name="type">型</param>
        /// <param name="val">値</param>
        public void AddData( Type type, object val )
        {
            lock( this._dataList )
            {
                this._dataList.Add( val );
            }
        }

        /// <summary>
        /// データリスト取得
        /// </summary>
        public List<object> GetData()
        {
            lock( this._dataList )
            {
                return this._dataList;
            }
        }

        /// <summary>
        /// パラメータを設定する
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="type">型</param>
        /// <returns></returns>
        public object GetData( int index, Type type )
        {
            try
            {
                lock( this._dataList )
                {
                    object val = new object();
                    val = Convert.ChangeType( this._dataList[index], type );
                    return val;
                }
            }
            catch( Exception ex )
            {
                Console.Write( ex );
                return null;
            }
        }

        /// <summary>
        /// パラメータデータを設定する(InterProcessから使用する)
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="type">型</param>
        /// <param name="val">値</param>
        public void SetData( int index, Type type, object val ) 
        {
            try
            {
                lock( this._dataList )
                {
                    this._dataList[index] = val;
                }
            }
            catch( Exception ex )
            {
                Console.Write( ex );
            }
        }

        /// <summary>
        /// 内部のデータリストを初期化する
        /// </summary>
        public void ClearDataList()
        {
            lock( this._dataList )
            {
                this._dataList.Clear();
            }
        }
    }
}
