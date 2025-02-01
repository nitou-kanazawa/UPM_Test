using System.Collections.Generic;
using UnityEngine;

// [参考]
//  qiita: Unityで使える便利関数(拡張メソッド)達 https://qiita.com/nmss208/items/9846525cf523fb961b48

namespace Nitou {

    /// <summary>
    /// <see cref="Component"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class ComponentExtensions {

        /// ----------------------------------------------------------------------------
        // コンポーネントの追加

        /// <summary>
        /// AddComponentの拡張メソッド．
        /// </summary>
        public static T AddComponent<T>(this Component self) where T : Component {
            return self.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// AddComponentsの拡張メソッド．
        /// </summary>
        public static void AddComponents<T1, T2>(this Component self)
            where T1 : Component where T2 : Component {
            self.gameObject.AddComponents<T1, T2>();
        }

        /// <summary>
        /// AddComponentsの拡張メソッド．
        /// </summary>
        public static void AddComponents<T1, T2, T3>(this Component self)
            where T1 : Component where T2 : Component where T3 : Component {
            self.gameObject.AddComponents<T1, T2, T3>();
        }

        /// <summary>
        /// GameObjectが対象のコンポーネント持つ場合はそれを取得し，なければ追加して返す拡張メソッド．
        /// </summary>
        public static T GetOrAddComponent<T>(this Component self) where T : Component {
            return self.gameObject.GetOrAddComponent<T>();
        }


        /// ----------------------------------------------------------------------------
        // コンポーネントの破棄

        /// <summary>
        /// Destoryの拡張メソッド．
        /// </summary>
        public static void Destroy(this Component self) {
            Object.Destroy(self);
        }

        /// <summary>
        /// DestroyImmediateの拡張メソッド．
        /// </summary>
        public static void DestroyImmediate(this Component self) {
            Object.DestroyImmediate(self);
        }

        /// <summary>
        /// ComponentがアタッチされているGameObjectを破棄する．
        /// </summary>
        public static void DestroyGameObject(this Component self) {
            Object.Destroy(self.gameObject);
        }


        /// ----------------------------------------------------------------------------
        // 設定

        public static void SetActive(this Component self, bool active) {
            self.gameObject.SetActive(active);
        }



        /// ----------------------------------------------------------------------------
        // 判定

        /// <summary>
        /// コンポーネントが有効かどうかを確認する拡張メソッド
        /// </summary>
        public static bool IsEnabled(this Component self) {
            var property = self.GetType().GetProperty("enabled", typeof(bool));
            return (bool)(property?.GetValue(self, null) ?? true);
        }

        /// <summary>
        /// GameObjectが対象のレイヤーに含まれているかを調べる拡張メソッド
        /// </summary>
        public static bool IsInLayerMask(this Component self, LayerMask layerMask) {
            return GameObjectExtensions.IsInLayerMask(self.gameObject, layerMask);
        }
    }


    /// <summary>
    /// <see cref="Behaviour"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class BehaviourExtensions {

        /// <summary>
        /// enabledとgameObject.activeSelfを一括で設定する拡張メソッド
        /// </summary>
        public static void SetActiveAndEnabled(this Behaviour self, bool value) {
            self.enabled = value;
            self.gameObject.SetActive(value);
        }

    }
}