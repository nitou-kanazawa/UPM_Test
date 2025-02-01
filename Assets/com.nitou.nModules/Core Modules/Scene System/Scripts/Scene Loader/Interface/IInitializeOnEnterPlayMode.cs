using UnityEngine;

namespace Nitou.SceneSystem{

    /// <summary>
    /// Interface to perform processing in EnterPlayMode.
    /// </summary>
    internal interface IInitializeOnEnterPlayMode{

        void OnEnterPlayMode();
    }
}
