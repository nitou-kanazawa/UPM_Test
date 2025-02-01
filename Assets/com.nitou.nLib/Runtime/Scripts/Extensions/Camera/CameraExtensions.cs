using System;
using System.Linq;
using UnityEngine;

// [REF] 
//  Unity Forums: Handles.Label with constant size (not scale based on distance to camera) 

namespace Nitou {

    /// <summary>
    /// <see cref="Camera"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class CameraExtensions {

        /// <summary>
        /// アタッチされているAudioListenerを取得する拡張メソッド
        /// </summary>
        public static AudioListener GetOrAddAudioListener(this Camera self) {
            if (self == null) {
                throw new ArgumentNullException(nameof(self));
            }
            return self.GetOrAddComponent<AudioListener>();
        }

        /// <summary>
        /// 指定したワールド座標がカメラ範囲内に収まっているか調べる
        /// </summary>
        public static bool ContaineWorldPosition(this Camera self, Vector3 position, float distance) {
            Vector3 screenPos = self.WorldToScreenPoint(position);
            return ((screenPos.x >= 0) &&
            (screenPos.x <= self.pixelWidth) &&
            (screenPos.y >= 0) &&
            (screenPos.y <= self.pixelHeight) &&
            (screenPos.z > 0) &&
            (screenPos.z < distance));
        }
    }
}
