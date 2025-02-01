using System;

// [参考]
//  _: 秒→時間　変換（hh:mm:ss変換） https://frog-blend.hatenablog.com/entry/2023/10/03/115056

namespace Nitou {

    // ※仮実装
    public static class TimeUtil {

        public static TimeSpan SecondToTimeSpan(int second) {
            return new TimeSpan(0, 0, second);
        }
    }
}
