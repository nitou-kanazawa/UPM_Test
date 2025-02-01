#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

// [参考]
//  コガネブログ: ParticleSystemのInspectorに"Play・Pause"などのボタンを追加するエディタ拡張 https://baba-s.hatenablog.com/entry/2022/02/22/090000

namespace Nitou.Tools.Inspector {
    using Nitou.EditorShared;

    /// <summary>
    /// ParticleSystemのインスペクター拡張
    /// </summary>
    [CustomEditor(typeof(ParticleSystem))]
    public sealed class ParticleSystemInspector : Editor {
        
        // オリジナルの拡張クラス
        private static readonly Type BASE_EDITOR_TYPE = typeof(Editor)
            .Assembly
            .GetType("UnityEditor.ParticleSystemInspector");

        /// <summary>
        /// インスペクタ描画
        /// </summary>
        public override void OnInspectorGUI() {

            // -------------
            // 拡張分のインスペクター表示

            var particleSystem = (ParticleSystem)target;

            using (new EditorGUILayout.HorizontalScope())
            //using (new EditorUtil.GUIColorScope(EditorColors.DefaultBackground)) 
                {

                if (!particleSystem.isPlaying) {
                    DrawPlayButton(particleSystem);
                } else {
                    DrawPauseButton(particleSystem);
                }
                
                // 停止
                if (GUILayout.Button("Stop")) {
                    particleSystem.Stop();
                }

                // クリア
                if (GUILayout.Button("Clear")) {
                    particleSystem.Clear();
                }
            }


            // -------------
            // オリジナルのインスペクター表示

            var editor = CreateEditor(particleSystem, BASE_EDITOR_TYPE);
            editor.OnInspectorGUI();
        }


        /// <summary>
        /// 再生ボタンを表示する
        /// </summary>
        private void DrawPlayButton(ParticleSystem particleSystem) {
            if (GUILayout.Button("Play")) {
                particleSystem.Play();
            }
        }

        /// <summary>
        /// ポーズボタンを表示する
        /// </summary>
        private void DrawPauseButton(ParticleSystem particleSystem) {
            if (GUILayout.Button("Pause")) {
                particleSystem.Pause();
            }
        }
    }
}
#endif