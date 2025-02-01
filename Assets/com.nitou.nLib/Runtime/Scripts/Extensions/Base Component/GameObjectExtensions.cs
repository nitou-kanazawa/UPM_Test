using System.Linq;
using UnityEngine;

// [REF]
//  コガネブログ: GetComponentsInChildrenで自分自身を含まないようにする拡張メソッド https://baba-s.hatenablog.com/entry/2014/06/05/220224
//  qiita: ちょっとだけ便利になるかもしれない拡張メソッド集 https://qiita.com/tanikura/items/ed5d56ebbfcad19c488d
//  kanのメモ帳: 拡張メソッドとは、全ての子オブジェクトにレイヤーとマテリアル設定を行ってみる https://kan-kikuchi.hatenablog.com/entry/GameObjectExtension

namespace Nitou {

    /// <summary>
    /// <see cref="GameObject"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        #region コンポーネント (有無)

        /// <summary>
        /// 指定されたコンポーネントがアタッチされているかどうかを確認する拡張メソッド
        /// </summary>
        public static bool HasComponent<T>(this GameObject self)
            where T : Component {
            return self.GetComponent<T>() != null;
        }

        /// <summary>
        /// 指定されたコンポーネントがアタッチされているかどうかを確認する拡張メソッド
        /// </summary>
        public static bool HasComponent(this GameObject self, System.Type type) {
            return self.GetComponent(type) != null;
        }

        /// <summary>
        /// 指定されたコンポーネントがアタッチされているかどうかを確認する拡張メソッド
        /// </summary>
        public static bool HasComponents<T1, T2>(this GameObject self)
            where T1 : Component where T2 : Component{
            return self.HasComponent<T1>() && self.HasComponent<T2>();
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region コンポーネント (削除)

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponent<T>(this GameObject self)
            where T : Component {
            T component = self.GetComponent<T>();
            if (component != null) Object.Destroy(component);
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponents<T1, T2>(this GameObject self)
            where T1 : Component where T2 : Component {
            self.RemoveComponent<T1>();
            self.RemoveComponent<T2>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponents<T1, T2, T3>(this GameObject self)
            where T1 : Component where T2 : Component where T3 : Component {
            self.RemoveComponents<T1, T2>();
            self.RemoveComponent<T3>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveComponents<T1, T2, T3, T4>(this GameObject self)
            where T1 : Component where T2 : Component where T3 : Component where T4 : Component {
            self.RemoveComponents<T1, T2, T3>();
            self.RemoveComponent<T4>();
            return self;
        }

        /// <summary>
        /// 全てのコンポーネントを削除する拡張メソッド
        /// </summary>
        public static GameObject RemoveAllComponents(this GameObject self) {
            foreach (var component in self.GetComponents<Component>()) {
                if (!(component is Transform)) {
                    Object.Destroy(component);
                }
            }

            return self;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region コンポーネント (追加)

        /// <summary>
        /// 指定されたコンポーネントを追加する拡張メソッド
        /// </summary>
        public static GameObject AddComponents<T1, T2>(this GameObject self)
            where T1 : Component where T2 : Component {
            self.AddComponentIfNotExists<T1>();
            self.AddComponentIfNotExists<T2>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを追加する拡張メソッド
        /// </summary>
        public static GameObject AddComponents<T1, T2, T3>(this GameObject self)
            where T1 : Component where T2 : Component where T3 : Component {
            self.AddComponents<T1, T2>();
            self.AddComponentIfNotExists<T3>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを追加する拡張メソッド
        /// </summary>
        public static GameObject AddComponents<T1, T2, T3, T4>(this GameObject self)
            where T1 : Component where T2 : Component where T3 : Component where T4 : Component {
            self.AddComponents<T1, T2, T3>();
            self.AddComponentIfNotExists<T4>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを追加する拡張メソッド
        /// </summary>
        public static GameObject AddComponentIfNotExists<T>(this GameObject self)
            where T : Component {
            // コンポーネントが存在しない場合のみ追加
            if (!self.HasComponent<T>()) {
                self.AddComponent<T>();
            }
            return self;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region コンポーネント（取得）

        /// <summary>
        /// 対象のコンポーネント持つ場合はそれを取得し，なければ追加して返す拡張メソッド
        /// </summary>
        public static T GetOrAddComponent<T>(this GameObject self)
            where T : Component {
            var component = self.GetComponent<T>();
            return component ?? self.AddComponent<T>();
        }

        /// <summary>
        /// 自分自身を含まないGetComponentsInChaidrenの拡張メソッド
        /// </summary>
        public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self)
            where T : Component {
            return self.GetComponentsInChildren<T>().Where(c => self != c.gameObject).ToArray();
        }

        /// <summary>
        /// GetComponentInChaildrenの拡張メソッド
        /// </summary>
        public static bool TryGetComponentInChildren<T>(this GameObject self, out T component)
            where T : Component {
            // 子要素から指定コンポーネントを取得する
            component = self.GetComponentInChildren<T>();
            return component != null;
        }

        /// <summary>
        /// Gets a "target" component within a particular branch (inside the hierarchy). The branch is defined by the "branch root object", which is also defined by the chosen 
        /// "branch root component". The returned component must come from a child of the "branch root object".
        /// </summary>
        public static T2 GetComponentInBranch<T1, T2>(this GameObject self, bool includeInactive = true) 
            where T1 : Component where T2 : Component {
            T1[] entryComponents = self.transform.root.GetComponentsInChildren<T1>(includeInactive);

            if (entryComponents.Length == 0) {
                Debug.LogWarning($"Root component: No objects found with {typeof(T1).Name} component");
                return null;
            }

            foreach(var entry in entryComponents) {

                // 直接的な親子関係にない場合は次へ
                if (!self.transform.IsChildOf(entry.transform) && !entry.transform.IsChildOf(self.transform))
                    continue;

                // entryを起点にコンポーネントを探索
                if (entry.gameObject.TryGetComponentInChildren<T2>(out var targetComponent)) {
                    return targetComponent;
                } 
            }

            return null;
        }

        /// <summary>
        /// Gets a "target" component within a particular branch (inside the hierarchy). The branch is defined by the "branch root object", which is also defined by the chosen 
        /// "branch root component". The returned component must come from a child of the "branch root object".
        /// </summary>
        public static T1 GetComponentInBranch<T1>(this GameObject self, bool includeInactive = true) 
            where T1 : Component {
            return self.GetComponentInBranch<T1, T1>(includeInactive);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region コンポーネント（有効状態）

        /// <summary>
        /// 指定されたコンポーネントを有効化する拡張メソッド
        /// </summary>
        public static GameObject EnableComponent<T>(this GameObject self)where T : Behaviour {
            if (self.HasComponent<T>()) {
                self.GetComponent<T>().enabled = true;
            }
            return self;
        }

        /// <summary>
        /// 指定された複数のコンポーネントを有効化する拡張メソッド
        /// </summary>
        public static GameObject EnableComponents<T1, T2>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour {
            self.EnableComponent<T1>();
            self.EnableComponent<T2>();
            return self;
        }

        /// <summary>
        /// 指定された3つのコンポーネントを有効化する拡張メソッド
        /// </summary>
        public static GameObject EnableComponents<T1, T2, T3>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour where T3 : Behaviour {
            self.EnableComponents<T1, T2>();
            self.EnableComponent<T3>();
            return self;
        }

        /// <summary>
        /// 指定された4つのコンポーネントを有効化する拡張メソッド
        /// </summary>
        public static GameObject EnableComponents<T1, T2, T3, T4>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour where T3 : Behaviour where T4 : Behaviour {
            self.EnableComponents<T1, T2, T3>();
            self.EnableComponent<T4>();
            return self;
        }

        /// <summary>
        /// 指定されたコンポーネントを非有効化する拡張メソッド
        /// </summary>
        public static GameObject DisableComponent<T>(this GameObject self) where T : Behaviour {
            if (self.HasComponent<T>()) {
                self.GetComponent<T>().enabled = false;
            }
            return self;
        }

        /// <summary>
        /// 指定された複数のコンポーネントを無効化する拡張メソッド
        /// </summary>
        public static GameObject DisableComponents<T1, T2>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour {
            self.DisableComponent<T1>();
            self.DisableComponent<T2>();
            return self;
        }

        /// <summary>
        /// 指定された3つのコンポーネントを無効化する拡張メソッド
        /// </summary>
        public static GameObject DisableComponents<T1, T2, T3>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour where T3 : Behaviour {
            self.DisableComponents<T1, T2>();
            self.DisableComponent<T3>();
            return self;
        }

        /// <summary>
        /// 指定された4つのコンポーネントを無効化する拡張メソッド
        /// </summary>
        public static GameObject DisableComponents<T1, T2, T3, T4>(this GameObject self)
            where T1 : Behaviour where T2 : Behaviour where T3 : Behaviour where T4 : Behaviour {
            self.DisableComponents<T1, T2, T3>();
            self.DisableComponent<T4>();
            return self;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 複製

        /// <summary>
        /// 対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject self) {
            return Object.Instantiate(self);
        }

        /// <summary>
        /// 生成後に親となるTransformを指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject self, Transform parent) {
            return Object.Instantiate(self, parent);
        }

        /// <summary>
        /// 生成後の座標及び姿勢を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject self, Vector3 pos, Quaternion rot) {
            return Object.Instantiate(self, pos, rot);
        }

        /// <summary>
        /// 生成後に親となるTransform、また生成後の座標及び姿勢を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject Instantiate(this GameObject self, Vector3 pos, Quaternion rot, Transform parent) {
            return Object.Instantiate(self, pos, rot, parent);
        }

        /// <summary>
        /// 生成後に親となるTransform、また生成後のローカル座標を指定して、対象のGameObjectを複製(生成)して返す拡張メソッド
        /// </summary>
        public static GameObject InstantiateWithLocalPosition(this GameObject self, Transform parent, Vector3 localPos) {
            var instance = Object.Instantiate(self, parent);
            instance.transform.localPosition = localPos;
            return instance;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 破棄

        /// <summary>
        /// Destroyの拡張メソッド
        /// </summary>
        public static void Destroy(this GameObject self) {
            Object.Destroy(self);
        }

        /// <summary>
        /// DestroyImmediateの拡張メソッド
        /// </summary>
        public static void DestroyImmediate(this GameObject self) {
            Object.DestroyImmediate(self);
        }

        /// <summary>
        /// 子オブジェクトをすべて破壊する拡張メソッド
        /// </summary>
        public static GameObject DestroyAllChildren(this GameObject self) {
            foreach (Transform child in self.transform) {
                Object.Destroy(child.gameObject);
            }
            return self;
        }

        /// <summary>
        /// DontDestroyOnLoadの拡張メソッド
        /// </summary>
        public static GameObject DontDestroyOnLoad(this GameObject self) {
            Object.DontDestroyOnLoad(self);
            return self;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // アクティブ状態

        /*

        /// <summary>
        /// アクティブ状態の切り替え設定を行う拡張メソッド
        /// </summary>
        public static System.IDisposable SetActiveSelfSource(this GameObject self, System.IObservable<bool> source, bool invert = false) {
            return source
                .Subscribe(x => {
                    x = invert ? !x : x;
                    self.SetActive(x);
                })
                .AddTo(self);
        }

        */


        /// ----------------------------------------------------------------------------
        // レイヤー

        /// <summary>
        /// 対象のレイヤーに含まれているかを調べる拡張メソッド
        /// </summary>
        public static bool IsInLayerMask(this GameObject self, LayerMask layerMask) {
            int objLayerMask = (1 << self.layer);
            return (layerMask.value & objLayerMask) > 0;
        }

        /// <summary>
        /// レイヤーを設定する拡張メソッド
        /// </summary>
        public static void SetLayer(this GameObject self, string layerName) {
            self.layer = LayerMask.NameToLayer(layerName);
        }

        /// <summary>
        /// レイヤーを設定する拡張メソッド
        /// </summary>
        public static void SetLayerRecursively(this GameObject self, int layer) {
            self.layer = layer;

            // 子のレイヤーにも設定する
            foreach (Transform childTransform in self.transform) {
                SetLayerRecursively(childTransform.gameObject, layer);
            }
        }

        /// <summary>
        /// レイヤーを設定する拡張メソッド
        /// </summary>
        public static void SetLayerRecursively(this GameObject self, string layerName) {
            self.SetLayerRecursively(LayerMask.NameToLayer(layerName));
        }


        /// ----------------------------------------------------------------------------
        // タグ

        /// <summary>
        /// 指定したタグ群に含まれているか調べる拡張メソッド
        /// </summary>
        public static bool ContainTag(this GameObject self, in string[] tagArray) {
            // ※タグが含まれない場合，trueを返す
            if (tagArray == null || tagArray.Length == 0) return true;

            for (var i = 0; i < tagArray.Length; i++) {
                if (self.CompareTag(tagArray[i])) return true;
            }

            return false;
        }


        /// ----------------------------------------------------------------------------
        // マテリアル

        /// <summary>
        /// マテリアル設定
        /// </summary>
        /// <param name="needSetChildrens">子にもマテリアル設定を行うか</param>
        public static void SetMaterial(this GameObject gameObject, Material setMaterial, bool needSetChildrens = true) {
            if (gameObject == null) {
                return;
            }

            //レンダラーがあればそのマテリアルを変更
            if (gameObject.GetComponent<Renderer>()) {
                gameObject.GetComponent<Renderer>().material = setMaterial;
            }

            //子に設定する必要がない場合はここで終了
            if (!needSetChildrens) return;

            //子のマテリアルにも設定する
            foreach (Transform childTransform in gameObject.transform) {
                SetMaterial(childTransform.gameObject, setMaterial, needSetChildrens);
            }

        }
    }
}
