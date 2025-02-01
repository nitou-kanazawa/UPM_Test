using System;
using System.Runtime.CompilerServices;

// [REF]
//  hatena: 実は関数を呼び出すのにコストがかかってた？！ https://sat-box.hatenablog.jp/entry/2022/05/20/133607

namespace Nitou {

    public static class Error {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNullException<T>(T value) {
            if (value == null) throw new ArgumentNullException();
        }
    }
}
