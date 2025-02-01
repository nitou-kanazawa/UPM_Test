using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [REF]
//  _: シングルトンなScriptableObjectを実装する https://mackysoft.net/singleton-scriptableobject/
//  qiita: Generics のメソッドで型情報を取得する https://qiita.com/TsuyoshiUshio@github/items/7b9544fbc338af5807f5
//  github: somedeveloper00/SingletonScriptableObject https://github.com/somedeveloper00/SingletonScriptableObject/blob/master/Runtime/src/Sample.cs

namespace Nitou.DesignPattern.Singltons {

    /// <summary>
    /// ScriptableObjectを継承したシングルトン
    /// </summary>
    public abstract class SingletonScriptableObject<T> : ScriptableObject
        where T : ScriptableObject {

        // 
        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {

                    // Resourceフォルダからアセットを取得
                    _instance = Resources.Load<T>(typeof(T).Name);

#if UNITY_EDITOR
                    // Assetsフォルダ内からアセットを取得
                    if (_instance == null) {
                        _instance = ScriptableObjectUtils.FindScriptableObject<T>();
                    }
#endif

                    // 存在しない場合はエラー
                    if (_instance == null) {
                        Debug.LogError(typeof(T) + " のアセットはフォルダ内に存在しません");
                    }
                }
                return _instance;
            }
        }


        /// ----------------------------------------------------------------------------
        // Lifecycle Events

        protected virtual void Awake() {
            if (!_instance || _instance == this) return;
            Debug.LogError($"{typeof(T).Name} deleted. Another instance is already available.");
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(this);
            else
#endif
                Destroy(this);
        }

        protected virtual void OnDestroy() {
            if (_instance == this) {
                Debug.LogWarning($"{typeof(T).Name} instance destroyed. Singleton instance is no longer available.");
            }
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// 他のゲームオブジェクトにアタッチされているか調べる
        /// </summary>
        protected bool CheckInstance() {
            // 存在しない（or自分自身）場合
            if (_instance == null) {
                _instance = this as T;
                return true;
            } else if (Instance == this) {
                return true;
            }
            // 既に存在する場合
            return false;
        }

    }

}
