#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Unity.CodeEditor;

// [参考]　
//  コガネブログ: baba-s/Kogane.InspectorHeaderGUI https://github.com/baba-s/Kogane.InspectorHeaderGUI/blob/master/Editor/InspectorHeaderGUI.cs#L70

namespace Nitou.Tools.Inspector {
    using Nitou.EditorShared;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;

    [InitializeOnLoad]
    internal static class InspectorHeaderGUI {

        private sealed class TextureData {
            private readonly string _guid;
            private GUIContent _guiContentCache;

            public GUIContent GuiContent {
                get {
                    if (_guiContentCache != null) return _guiContentCache;

                    var path = AssetDatabase.GUIDToAssetPath(_guid);
                    var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                    _guiContentCache = new(texture) {
                        text = string.Empty,
                    };

                    return _guiContentCache;
                }
            }

            public TextureData(string guid) {
                _guid = guid;
            }
        }

        private static readonly Type PROPERTY_EDITOR_TYPE = typeof(Editor).Assembly.GetType("UnityEditor.PropertyEditor");

        // アイコン
        private static readonly TextureData LOCK_TEXTURE = new("e46fc48e73c498149be679eb9513bef1");
        private static readonly TextureData UNLOCK_TEXTURE = new("3bc24d4385a34454cb8e67f037925c93");
        private static readonly TextureData DEBUG_TEXTURE = new("bad1644c13e0c694382f5b3d51bbeba7");
        private static readonly TextureData PROPERTIES_TEXTURE = new("f06580a50a2eeae4f990a75bd4ff9f2c");
        private static readonly TextureData PASTE_COMPONENT_AS_NEW_TEXTURE = new("45d577067ad153d41ba8159fda1dac09");
        private static readonly TextureData REVEAL_IN_FINDER_TEXTURE = new("3a4a8645203241445804d6b3e0614b39");
        private static readonly TextureData EXPAND_ALL_COMPONENTS_TEXTURE = new("ff490dff6b933244c862ec4ffb5f0966");
        private static readonly TextureData COLLAPSE_ALL_COMPONENTS_TEXTURE = new("9c38d887be2f708418388db6a513aa78");
        private static readonly TextureData VS_CODE_TEXTURE = new("642d88ffa9946e143b3fc51b286b6ad7");
        private static readonly TextureData META_TEXTURE = new("2f897b3428dafca4dbc7b2d661fb2099");
        private static readonly TextureData PHOTOSHOP_TEXTURE = new("9cbc2d462f057664ab32a2a81a7738a7");
        private static readonly TextureData RIDER_TEXTURE = new("a466de63f2a2a0a46a1ccebfa4b25cf3");
        private static readonly TextureData PING_TEXTURE = new("5f2e5c5ccaf62bb4d8043be19cc9fc5c");


        static InspectorHeaderGUI() {
            Editor.finishedDefaultHeaderGUI -= OnGUI;
            Editor.finishedDefaultHeaderGUI += OnGUI;
        }

        private static void OnGUI(Editor editor) {

            // [FIXME] 
            // マテリアルのインスペクタにはカスタムGUIを表示しない
            if (editor.target is Material || editor.target is DefaultAsset) {
                return;
            }

            using (new EditorGUILayout.HorizontalScope())
            using (new EditorUtil.GUIContentColorScope(Colors.White)) {

                // 基本操作ボタン
                DrawLockButton();
                DrawDebugButton();
                DrawPingButton(editor.target);
                DrawPropertiesButton();


                using (new EditorUtil.EnableScope(editor.targets.All(x => !EditorUtility.IsPersistent(x)))) {
                    DrawPasteComponentAsNew(editor);
                    DrawOpenMetaButton(editor);
                    DrawOpenVisualStudioCodeButton(editor);
                    DrawRevealInFinderButton(editor);
                }
            }
            DrawGuidLabel(editor);
        }


        /// ----------------------------------------------------------------------------
        #region Drawing Method

        /// <summary>
        /// ロックボタンを表示する
        /// </summary>
        private static void DrawLockButton() {
            using var enableScope = new EditorUtil.EnableScope(true);

            var tracker = ActiveEditorTracker.sharedTracker;

            var content = tracker.isLocked ? LOCK_TEXTURE.GuiContent : UNLOCK_TEXTURE.GuiContent;
            if (GUILayout.Button(content, EditorStyles.miniButtonLeft)) {
                tracker.isLocked = !tracker.isLocked;
                tracker.ForceRebuild();
            }
        }

        /// <summary>
        /// デバッグモード切り替えボタンを表示する
        /// </summary>
        private static void DrawDebugButton() {
            using var enableScope = new EditorUtil.EnableScope(true);

            var tracker = ActiveEditorTracker.sharedTracker;
            var isNormal = tracker.inspectorMode == InspectorMode.Normal;

            if (!GUILayout.Button(DEBUG_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) return;

            // Inspectorの取得
            var editorWindowArray = Resources.FindObjectsOfTypeAll<EditorWindow>();
            var inspectorWindow = ArrayUtility.Find(editorWindowArray, x => x.GetType().Name == "InspectorWindow");
            if (inspectorWindow == null) return;

            var inspectorWindowType = inspectorWindow.GetType();
            var propertyEditorType = inspectorWindowType.BaseType;
            Debug.Assert(propertyEditorType != null, nameof(propertyEditorType) + " != null");

            var propertyInfo = propertyEditorType.GetProperty
            (
                name: "inspectorMode",
                bindingAttr: BindingFlags.Public | BindingFlags.Instance
            );
            Debug.Assert(propertyInfo != null, nameof(propertyInfo) + " != null");

            // 1 フレーム遅らせないと以下のエラーが発生する
            // EndLayoutGroup: BeginLayoutGroup must be called first.
            EditorApplication.delayCall += () => {
                propertyInfo.SetValue(inspectorWindow, isNormal ? InspectorMode.Debug : InspectorMode.Normal);
                tracker.ForceRebuild();
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private static void DrawPingButton(UnityEngine.Object target) {
            using var enableScope = new EditorUtil.EnableScope(true);

            if (GUILayout.Button(PING_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) {
                EditorGUIUtility.PingObject(target);
            }
        }

        /// <summary>
        /// プロパティボタンを表示する
        /// </summary>
        private static void DrawPropertiesButton() {
            using var enableScope = new EditorUtil.EnableScope(true);

            if (GUILayout.Button(PROPERTIES_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) {
                EditorApplication.ExecuteMenuItem("Assets/Properties...");
            }
        }

        /// <summary>
        /// コンポーネントの貼付ボタンを表示する
        /// </summary>
        private static void DrawPasteComponentAsNew(Editor editor) {
            var enabled = editor.targets.All(x => x is GameObject);
            using var enableScope = new EditorUtil.EnableScope(enabled);

            if (GUILayout.Button(PASTE_COMPONENT_AS_NEW_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) {
                foreach (var gameObject in editor.targets.OfType<GameObject>()) {
                    ComponentUtility.PasteComponentAsNew(gameObject);
                }
            }
        }

        private static void DrawOpenMetaButton(Editor editor) {
            var enabled = editor.targets.All(x => EditorUtility.IsPersistent(x));
            using var enableScope = new EditorUtil.EnableScope(enabled);

            if (GUILayout.Button(META_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) {
                if (!EditorSettings.projectGenerationUserExtensions.Contains("meta")) {
                    Debug_.LogWarning("Project Settings の「Editor > Additional extensions to include」に `meta` を追加してください");
                    return;
                }

                foreach (var target in editor.targets) {
                    var assetPath = AssetDatabase.GetAssetPath(target);
                    var metaPath = $"{assetPath}.meta";

                    CodeEditor.CurrentEditor.OpenProject(metaPath);
                }
            }

        }

        private static void DrawOpenVisualStudioCodeButton(Editor editor) {
            var enabled = editor.targets.All(x => EditorUtility.IsPersistent(x));
            using var enableScope = new EditorUtil.EnableScope(enabled);


            if (GUILayout.Button(VS_CODE_TEXTURE.GuiContent, EditorStyles.miniButtonMid)) {
                foreach (var target in editor.targets) {
                    var assetPath = AssetDatabase.GetAssetPath(target);
                    var fullPath = Path.GetFullPath(assetPath);

                    var startInfo = new ProcessStartInfo("code", $@"-r ""{fullPath}""") {
                        WindowStyle = ProcessWindowStyle.Hidden,
                    };

                    try {
                        Process.Start(startInfo);
                    } catch (Win32Exception) {
                        Debug_.LogError("Mac でこのコマンドを使用する場合は Visual Studio Code のコマンドパレットで `Shell Command: Install code command in PATH` を実行しておく必要があります");
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private static void DrawRevealInFinderButton(Editor editor) {
            var enabled = editor.targets.All(x => EditorUtility.IsPersistent(x));
            using var enableScope = new EditorUtil.EnableScope(enabled);

            if (GUILayout.Button(REVEAL_IN_FINDER_TEXTURE.GuiContent, EditorStyles.miniButtonRight)) {
                foreach (var target in editor.targets) {
                    var assetPath = AssetDatabase.GetAssetPath(target);
                    EditorUtility.RevealInFinder(assetPath);
                }
            }
        }
        #endregion


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// GUIDを表示する
        /// </summary>
        /// <param name="editor"></param>
        private static void DrawGuidLabel(Editor editor) {
            if (editor.targets.Any(x => !EditorUtility.IsPersistent(x))) return;

            var assetPath = AssetDatabase.GetAssetPath(editor.target);
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var totalRect = EditorGUILayout.GetControlRect();

            // 
            var controlRect = EditorGUI.PrefixLabel(totalRect, EditorGUIUtility.TrTempContent("GUID"));

            if (1 < editor.targets.Length) {
                var label = EditorGUIUtility.TrTempContent("[Multiple objects selected]");
                EditorGUI.LabelField(controlRect, label);
            } else {
                EditorGUI.SelectableLabel(controlRect, guid);
            }
        }

        private static EditorWindow GetPropertyEditor() {
            return Resources
                    .FindObjectsOfTypeAll(PROPERTY_EDITOR_TYPE)
                    .FirstOrDefault() as EditorWindow
                ;
        }
    }
}
#endif