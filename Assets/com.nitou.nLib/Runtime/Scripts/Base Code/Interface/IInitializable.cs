
// [NOTE]
//  任意のタイミングでリセットしたいことが多くあるため，インターフェースを作成．
//  親オブジェクトから再帰的に処理することが主目的．

namespace Nitou {

    public interface IInitializable {

        /// <summary>
        /// 初期化が完了しているかどうか．
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// 初期化処理．
        /// </summary>
        public void Initialize();
    }


    public interface IInitializable<T> {

        /// <summary>
        /// 初期化が完了しているかどうか．
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// 初期化処理．
        /// </summary>
        public void Initialize(T item);
    }


    public interface IInitializable<T1, T2> {

        /// <summary>
        /// 初期化が完了しているかどうか．
        /// </summary>
        public bool IsInitialized { get; }

        /// <summary>
        /// 初期化処理．
        /// </summary>
        public void Initialize(T1 item1, T2 item2);
    }
}
