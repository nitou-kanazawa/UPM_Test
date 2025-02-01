
// [参考]
//  ねこじゃらシティ: スクリプトの実行順序を制御する https://nekojara.city/unity-script-execution-order
//  テラシュール: コンポーネントのイベント実行順についてのTips https://tsubakit1.hateblo.jp/entry/2017/02/05/003714

namespace Nitou {

    public static partial class GameConfigs{

        /// <summary>
        /// ExecutionOrderを指定するための静的クラス（※オーダー値の設定に関しては模索中）
        /// </summary>
        public static class ExecutionOrder {

            public const int VERY_FARST = -1000;
            public const int FARST = -100;
            public const int LATE = 100;
        }


    }
}
