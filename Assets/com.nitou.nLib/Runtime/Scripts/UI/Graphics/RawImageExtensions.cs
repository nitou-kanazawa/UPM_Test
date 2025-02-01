using System;
using UnityEngine;
using UnityEngine.UI;

namespace Nitou {

    /// <summary>
    /// <see cref="RawImage"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class RawImageExtensions {

        /// <summary>
        /// テクスチャサイズに基づいて指定したモードでアスペクト比を設定する．
        /// </summary>
        public static void SetAspectRatio(this RawImage self, AspectRatioFitter.AspectMode aspectMode) {

            if(self.texture == null)
                throw new InvalidOperationException("The texture assigned to the RawImage is null. Please assign a texture before setting the aspect ratio.");

            var aspectRatioFitter = self.GetOrAddComponent<AspectRatioFitter>();
            aspectRatioFitter.aspectMode = aspectMode;
            aspectRatioFitter.aspectRatio = (float)self.texture.width / self.texture.height;
        }

    }
}
