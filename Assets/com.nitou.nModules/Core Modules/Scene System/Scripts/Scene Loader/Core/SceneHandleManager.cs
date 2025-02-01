using UnityEngine;

// [REF] 
//  LIGHT11: シーンのロードと初期化タイミングをちゃんと理解する https://light11.hatenadiary.com/entry/2022/02/24/202754

namespace Nitou.SceneSystem{

    /// <summary>
    /// シーン読み込みを管理するクラス．
    /// </summary>
    internal class SceneHandleManager : IInitializeOnEnterPlayMode {

        /// <summary>
        /// コンストラクタ．
        /// </summary>
        public SceneHandleManager() {
            SceneInitialization.Register(this);
        }

        /// <summary>
        /// 
        /// </summary>
        void IInitializeOnEnterPlayMode.OnEnterPlayMode() {
            throw new System.NotImplementedException();
        }




    }
}
