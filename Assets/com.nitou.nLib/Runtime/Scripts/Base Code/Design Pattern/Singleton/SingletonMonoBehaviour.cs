using System.Linq;
using UnityEngine;

// [参考]
//  qiita: シングルトンを使ってみよう　https://qiita.com/Teach/items/c146c7939db7acbd7eee
//  kanのメモ帳: MonoBehaviourを継承したシングルトン https://kan-kikuchi.hatenablog.com/entry/SingletonMonoBehaviour

namespace Nitou.DesignPattern {

    /// <summary>
    /// MonoBehaviourを継承したシングルトン
    /// ※DontDestroyOnLoadは呼ばない（派生クラス側で行う）
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
                    // シーン内からインスタンスを取得
#if UNITY_2022_1_OR_NEWER
                    _instance = FindObjectsByType<T>(FindObjectsSortMode.None).First(); 
#else
                    _instance = FindObjectsOfType<T>(includeInactive: true).First(); 
#endif

                    // シーン内に存在しない場合はエラー
                    if (_instance == null) {
                        Debug.LogError(typeof(T) + " をアタッチしているGameObjectはありません");
                    }
                }
                return _instance;
            }
        }


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected virtual void Awake() {
            // 既にインスタンスが存在するなら破棄
            if (!CheckInstance())
                Destroy(this.gameObject);
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


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void Reset() {

        }
#endif
    }

}