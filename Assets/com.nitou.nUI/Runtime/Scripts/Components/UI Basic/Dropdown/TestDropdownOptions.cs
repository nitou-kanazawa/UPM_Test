using System;
using UnityEngine;
using TMPro;

namespace Nitou.UI.Demo {

    public enum MyType {
        ModeA,　ModeB,　ModeC,　ModeD,
    }

    // 派生クラス
    public sealed class TestDropdownOptions : DropdownEnumOptions<MyType> {}
}