using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpConnect.InternalData.NIS_Elements
{
    /// <summary>
    /// ND Experiment実行情報
    /// </summary>
    public class NDExperimentInfo
    {
        /// <summary>
        /// T有効/無効
        /// </summary>
        public bool enableT { get; set; }
        /// <summary>
        /// XY有効/無効
        /// </summary>
        public bool enableXY { get; set; }
        /// <summary>
        /// Z有効/無効
        /// </summary>
        public bool enableZ { get; set; }
        /// <summary>
        /// Tのループ数合計
        /// </summary>
        public List<int> loopT { get; set; } = new List<int>();
        /// <summary>
        /// XY数
        /// </summary>
        public int XYCount { get; set; }
        /// <summary>
        /// ZStep数
        /// </summary>
        public int ZStepCount { get; set; }
        /// <summary>
        /// 使用Chビット
        /// </summary>
        public int UseChannelBits { get; set; }
        /// <summary>
        /// 総撮像回数
        /// </summary>
        public int TotalImagingCount { get; set; }
    }
}
