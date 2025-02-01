using UnityEngine;

namespace Nitou.UI.Components {

    public interface IIconView {

        /// <summary>
        /// Set sprite.
        /// </summary>
        public void SetSprite(Sprite sprite);

        /// <summary>
        /// Set size of sprite.
        /// </summary>
        public void SetNativeSize(float scale);
    }
}
