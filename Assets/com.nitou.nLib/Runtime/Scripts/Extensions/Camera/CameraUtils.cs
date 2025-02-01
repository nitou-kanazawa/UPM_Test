using System.Collections.Generic;
using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Camera"/>型を対象とした汎用メソッド集．
    /// </summary>
    public static class CameraUtils {

        /// <summary>
        /// 登録されている全ての<see cref="Camera">カメラ</see>を非アクティブ状態に設定する
        /// </summary>
        public static void DeactivateAllCamera() {
            Camera.allCameras.ForEach(c => c.gameObject.SetActive(false));
        }

        /// <summary>
        /// 登録されている全ての<see cref="AudioListener">オーディオリスナー</see>を非アクティブ状態に設定する
        /// </summary>
        public static void DeactivateAllAudioListeners() {
            Camera.allCameras.ForEach(x => x.GetOrAddAudioListener().enabled = false);
        }
    }
}
