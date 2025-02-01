using System;
using UnityEngine;
using Sirenix.OdinInspector;

// [REF]
//  qiita: Unityで独自の設定のUIを提供できるSettingsProviderの紹介と設定ファイルの保存について https://qiita.com/sune2/items/a88cdee6e9a86652137c

namespace Nitou.Settings {

    /// <summary>
    /// Runtimeで参照するプロジェクト固有の設定データ．
    /// </summary>
    public sealed class ProjectSettingsSO : ScriptableObject {

        #region Singleton
        private static ProjectSettingsSO _instance;
        public static ProjectSettingsSO Instance {
            get {
                // [NOTE] Resources直下にクラス名と同名で配置されている必要がある．
                if (_instance == null)
                    _instance = Resources.Load<ProjectSettingsSO>(nameof(ProjectSettingsSO));

                if (_instance == null)
                    throw new InvalidOperationException($"{nameof(ProjectSettingsSO)} could not be loaded from the Resources folder. Please ensure it is properly placed.");

                return _instance;
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------

        [Title(" ")]
        [Indent] public bool executeAppLauncher;
        [Indent] public string text;

        [Title("UI")]

        [SerializeField] Vector2 _referenceResolution = new Vector2(1920, 1080);
        
        [SerializeField] int _screenCanvasSortingOrder = 10;
        [SerializeField] int _overlayCanvasSortingOrder = 100;



        /// ----------------------------------------------------------------------------

        /// <summary>
        /// 基準解像度
        /// </summary>
        public Vector2 ReferenceResolution => _referenceResolution;

        /// <summary>
        /// 通常キャンバスの描画順
        /// </summary>
        public int ScreenCanvasSortingOrder => _screenCanvasSortingOrder;

        /// <summary>
        /// オーバーレイキャンバスの描画順
        /// </summary>
        public int OverlayCanvasSortingOrder => _overlayCanvasSortingOrder;
    }

}
