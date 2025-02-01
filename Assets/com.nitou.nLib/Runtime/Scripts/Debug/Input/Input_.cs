using System.Collections.Generic;
using UnityEngine;

// [TODO] 流石に雑すぎるのでもう少しユースケースを検討してから修正する (2024.08.01)

namespace Nitou{

    /// <summary>
    /// テストコード用の入力関数を提供する静的クラス
    /// </summary>
    public static class Input_{

        public static bool KeyDown_Return() => Input.GetKeyDown(KeyCode.Return);
        public static bool KeyDown_Enter() => Input.GetKeyDown(KeyCode.KeypadEnter);
        public static bool KeyDown_Space() => Input.GetKeyDown(KeyCode.Space);
        public static bool KeyDown_Escape() => Input.GetKeyDown(KeyCode.Escape);
        
        public static bool KeyDown_A() => Input.GetKeyDown(KeyCode.A);
        public static bool KeyDown_D() => Input.GetKeyDown(KeyCode.D);
        public static bool KeyDown_J() => Input.GetKeyDown(KeyCode.J);
        public static bool KeyDown_K() => Input.GetKeyDown(KeyCode.K);
        public static bool KeyDown_L() => Input.GetKeyDown(KeyCode.L);
    }
}
