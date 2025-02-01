using UnityEngine;

namespace Nitou {

    /// <summary>
    /// "Create Asset Menu"用の各種定義
    /// </summary>
    public static class GameObjectMenu {

        /// ----------------------------------------------------------------------------
        // 接頭辞
        public static class Prefix {

            /// <summary>
            /// ダミーオブジェクト
            /// </summary>
            public const string DammyObject = "GameObject/Dammy Object/";

            /// <summary>
            /// シーン関連
            /// </summary>
            public const string SceneSystem = "GameObject/Scene System/";

            /// <summary>
            /// アニメーション関連
            /// </summary>
            public const string Animation = "GameObject/Animation/";

            /// <summary>
            /// 
            /// </summary>
            public const string Line = "GameObject/Line/";

        }


        /// ----------------------------------------------------------------------------
        // 接尾辞
        public static class Suffix {

        }


        /// ----------------------------------------------------------------------------
        // 表示順
        public static class Order {

            public const int DammyObject = 0;

        }



        /// ----------------------------------------------------------------------------

        //public class CreationScope : System.IDisposable {

        //    public CreationScope(System.Func<GameObject>) {

        //    }

        //    public void Dispose() {
        //        throw new System.NotImplementedException();
        //    }
        //}

    }
}
