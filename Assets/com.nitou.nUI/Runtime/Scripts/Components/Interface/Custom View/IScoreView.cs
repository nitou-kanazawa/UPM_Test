using UnityEngine;

namespace Nitou.UI.Components {

    /// <summary>
    /// UI interface with score indication.
    /// </summary>
    public interface IScoreTextView{

        /// <summary>
        /// Set score value.
        /// </summary>
        public void SetScore(int value);

        /// <summary>
        /// Set default score value.
        /// </summary>
        public void SetDefaule();
    }

}