using UnityEngine;

// [REF]
//  Hatena: C#でGenericなSingleton https://waken.hatenablog.com/entry/2016/03/05/102928
//  Zonn: シングルトンパターン（Singleton Pattern） https://zenn.dev/twugo/books/21cb3a6515e7b8/viewer/c52658
//  Qiita: Unityで学ぶデザインパターン05: Singleton パターン【デザパタ】https://qiita.com/Cova8bitdot/items/29b7064c7472a6f34972

namespace Nitou.DesignPattern.Singltons {

    /// <summary>
    /// シンプルなシングルトン (※実装サンプル)
    /// </summary>
    public class Singlton {

        public static Singlton Instance => _instance;
        private static Singlton _instance = new();

        private Singlton() { }
    }


    /// <summary>
    /// ジェネリックなシングルトン．
    /// </summary>
    public class Singleton<T> where T : class, new() {

        public static T Instance => _instance;
        private static readonly T _instance = new();

        // 万一、外からコンストラクタを呼ばれたときに、ここで引っ掛ける
        protected Singleton() {
            Debug.Assert(null == _instance);
        }

        /// <summary>
        /// インスタンスが存在するか確認する
        /// </summary>
        public static bool HasInstance() => _instance != null;
    }
}
