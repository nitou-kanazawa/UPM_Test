using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Nitou.UI.Overlay{

    /// <summary>
    /// オーバーレイ画面のインターフェース．
    /// </summary>
    public interface IUIOverlay{

        /// <summary>
        /// Progress: 1→0の画面遷移アニメーション
        /// </summary>
        public UniTask OpenAsync(float duration = 1f);

        /// <summary>
        /// Progress: 0→1の画面遷移アニメーション
        /// </summary>
        public UniTask CloseAsync(float duration = 1f);
    }


    /// <summary>
    /// <see cref="IUIOverlay"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class IUIOverlayExtensions {

        /// <summary>
        /// Progress: 0→1→0の画面遷移アニメーション
        /// </summary>
        public static async UniTask PlayAllAsync(this IUIOverlay overlay, 
            float closeDuration = 0.5f, float waitDuration = 1f, float openDuration = 0.5f,
            CancellationToken cancellationToken = default) {

            if (overlay == null) throw new System.ArgumentNullException(nameof(overlay));

            // 
            await overlay.CloseAsync(closeDuration);
            await UniTask.WaitForSeconds(waitDuration, true, cancellationToken: cancellationToken);
            await overlay.OpenAsync(openDuration);
        }
    }
}
