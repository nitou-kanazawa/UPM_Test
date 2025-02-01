using UnityEngine;
using UnityEngine.Playables;

namespace Nitou.BachProcessor {

    /// <summary>
    /// システムの更新タイミング．
    /// </summary>
    public enum UpdateTiming : int {
        Update = 0,
        FixedUpdate = 1,
        LateUpdate = 2,
    }


    /// <summary>
    /// 
    /// </summary>
    public abstract class UpdateTimingSingletonSO<TSystem> : ScriptableObject
        where TSystem : UpdateTimingSingletonSO<TSystem> {


        // ----- 

        /// <summary>
        /// Execution timing.
        /// </summary>
        public UpdateTiming Timing { get; private set; }

        /// <summary>
        /// Callback called when an instance is created.
        /// This is used to avoid interference with subclasses when implemented in Awake.
        /// It is called after Awake.
        /// </summary>
        protected virtual void OnCreate(UpdateTiming timing) { }

        /// <summary>
        /// Destroy the instance when the application is quitting.
        /// This is to handle EnterPlayMode.
        /// </summary>
        private void OnQuit() {
            Application.quitting -= OnQuit;
            
            // アプリ終了時に自身を破棄する．
            DestroyImmediate(this);
        }


        /// ----------------------------------------------------------------------------
        #region Static

        /// <summary>
        /// <see cref="UpdateTiming"/>の各タイミングをサポートするためのインスタンス．
        /// </summary>
        private static readonly TSystem[] _instances = new TSystem[3];

        /// <summary>
        /// インスタンスが生成済みか確認する　（※生成はしない）
        /// </summary>
        public static bool IsCreated(UpdateTiming timing) 
            => _instances[(int)timing] != null;

        /// <summary>
        /// インスタンスを取得する．
        /// 存在しなければ，生成する．
        /// </summary>
        public static TSystem GetInstance(UpdateTiming timing) {
            var index = (int)timing;
            if (IsCreated(timing)) return _instances[index];

            // Create instance
            var instance = ScriptableObject.CreateInstance<TSystem>();
            instance.Timing = timing;
            instance.OnCreate(timing);
            Application.quitting += instance.OnQuit;

            _instances[index] = instance;

            return instance;
        }
        #endregion
    }
}
