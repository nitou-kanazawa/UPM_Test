
namespace Nitou {

    public interface IPauseable{

        /// <summary>
        /// ポーズ状態かどうか．
        /// </summary>
        public bool IsPaused { get; }

        /// <summary>
        /// ポーズ処理．
        /// </summary>
        public void OnPause();

        /// <summary>
        /// ポーズ解除処理．
        /// </summary>
        public void Unpause();
    }
}