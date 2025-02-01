
namespace Nitou{

    /// <summary>
    /// 正規化された値 (0~1) に基づいて駆動するオブジェクト
    /// </summary>
    public interface INormalizedValueTicker {

        /// <summary>
        /// 正規化された値
        /// </summary>
        public NormalizedValue Rate { get; set; }
    }
}
