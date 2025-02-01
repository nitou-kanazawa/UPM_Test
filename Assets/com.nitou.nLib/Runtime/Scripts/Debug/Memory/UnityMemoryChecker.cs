using UnityEngine;
using UnityEngine.Profiling;

// [REF]
//  note: メモリ使用量を考える https://note.com/extrier/n/n2b55ba09856f
//  コガネブログ: Unity が確保したメモリの使用状況（Unity の使用メモリ）を取得するスクリプト https://baba-s.hatenablog.com/entry/2019/03/26/084000#google_vignette
//  UniDoc: Profiler https://docs.unity3d.com/ja/2023.2/ScriptReference/Profiling.Profiler.html
//  UniDoc: Memory Profiler モジュール https://docs.unity3d.com/ja/current/Manual/ProfilerMemory.html

namespace Nitou {

    /// <summary>
    /// メモリ使用量を計測するためのクラス．
    /// </summary>
    public sealed class UnityMemoryChecker {

        public float Used { get; private set; }
        public float Unused { get; private set; }
        public float Total { get; private set; }

        public string UsedText { get; private set; }
        public string UnusedText { get; private set; }
        public string TotalText { get; private set; }


        public void Update() {
            // Unity によって割り当てられたメモリ
            Used = (Profiler.GetTotalAllocatedMemoryLong() >> 10) / 1024f;

            // 予約済みだが割り当てられていないメモリ
            Unused = (Profiler.GetTotalUnusedReservedMemoryLong() >> 10) / 1024f;

            // Unity が現在および将来の割り当てのために確保している総メモリ
            Total = (Profiler.GetTotalReservedMemoryLong() >> 10) / 1024f;

            UsedText = Used.ToString("0.0") + " MB";
            UnusedText = Unused.ToString("0.0") + " MB";
            TotalText = Total.ToString("0.0") + " MB";
        }
    }
}
