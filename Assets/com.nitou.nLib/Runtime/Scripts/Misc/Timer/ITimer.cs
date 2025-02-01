
namespace Nitou{

    /// <summary>
    /// タイマーの基本操作を定義したインターフェース
    /// </summary>
    public interface ITimer {

        /// <summary>
        /// 開始する
        /// </summary>
        public void Start();
        
        /// <summary>
        /// 停止する
        /// </summary>
        public void Stop();

        /// <summary>
        /// リセットする
        /// </summary>
        public void Reset();
    }
}
