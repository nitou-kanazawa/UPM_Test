using UnityEngine.Profiling;

// [参考]
//  Unity Document: Profiler.BeginSample https://docs.unity3d.com/ja/current/ScriptReference/Profiling.Profiler.BeginSample.html

namespace Nitou {

    public readonly struct ProfilerScope : System.IDisposable{

        public ProfilerScope(string name) {
            Profiler.BeginSample(name);
        }

        public void Dispose() {
            Profiler.EndSample();
        }
    }
}
