using System;
using Cysharp.Threading.Tasks;
using UniRx;

namespace Nitou.GameSystem {

    public enum ProcessState {
        NotStarted,
        Running,
        Paused,
        Cancelled,
        Completed
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IProcess : IDisposable{

        /// <summary>
        /// 現在のステート．
        /// </summary>
        public IReadOnlyReactiveProperty<ProcessState> State { get; }

        /// <summary>
        /// 終了時の通知．
        /// </summary>
        public UniTask<ProcessResult> ProcessFinished { get; }

        /// <summary>
        /// 開始する．
        /// </summary>
        public void Run();
        
        /// <summary>
        /// ポーズする．
        /// </summary>
        public void Pause();

        /// <summary>
        /// ポーズ解除する．
        /// </summary>
        public void UnPause();

        /// <summary>
        /// キャンセルする．
        /// </summary>
        public void Cancel(CancelResult cancelResult = null);
    }

}
