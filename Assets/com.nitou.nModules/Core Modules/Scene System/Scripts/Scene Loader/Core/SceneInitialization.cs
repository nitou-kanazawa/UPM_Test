using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [REF]
//  UnityDoc: InitializeOnEnterPlayModeAttribute https://docs.unity3d.com/ja/2023.2/ScriptReference/InitializeOnEnterPlayModeAttribute.html

namespace Nitou.SceneSystem{

    /// <summary>
    /// 登録されたクラスの初期化処理を行う．
    /// </summary>
    internal class SceneInitialization {

        private static readonly List<IInitializeOnEnterPlayMode> _initializes = new();

        public static void Register(IInitializeOnEnterPlayMode initializer) {
            _initializes.AddIfNotContains(initializer);
        }

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void InitializeOnEnterPlayMode() {
            _initializes.ForEach(i => i.OnEnterPlayMode());
        }
#endif
    }
}
