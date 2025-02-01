
namespace Nitou {

    /// <summary>
    /// UnityEditorのToolbarメニューに関する定義
    /// </summary>
    public static class ToolBarMenu {

        /// ----------------------------------------------------------------------------
        // 接頭辞
        public static class Prefix {

            /// <summary>
            /// 
            /// </summary>
            public const string EditorWindow = "Window/Nitou/";

            /// <summary>
            /// 
            /// </summary>
            public const string EditorTool = "Tools/Nitou/";

            /// <summary>
            /// 開発中のデバッグコマンドなど用のタグ
            /// </summary>
            public const string Develop = "Develop/";


        }



        /// ----------------------------------------------------------------------------
        // 表示順
        public static class Order {
            public const int VeryEarly = 100;
            public const int Early = 50;
            public const int Normal = 0;
            public const int Late = -50;
            public const int VeryLate = -100;
        }
    }
}
