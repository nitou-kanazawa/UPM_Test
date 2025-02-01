using System;
using System.IO;
using UnityEngine;

// [参考]
//  _:絶対パスと Assets パスの変換メソッド https://www.create-forever.games/unity-absolute-path-assets-path/#google_vignette

namespace Nitou {

    // [基本機能]
    // アセットパスの保持: アセットパスを表す文字列を保持する。
    // パスの変換: AssetDatabase用のアセットパスと、System.IO用のファイルシステムパスとの相互変換が簡単に行える。
    // パス操作: パスの結合、相対パスと絶対パスの変換、拡張子の変更や削除など、基本的なパス操作が可能。
    // パスの検証: アセットパスが正しいフォーマットであるか、存在するかを検証できる。

    [System.Serializable]
    public sealed class AssetPath : IUnityProjectPath{

        // "Assets/以下の相対パス"
        [SerializeField] private string _relativePath = "";


        /// <summary>
        /// コンストラクタ（※非公開）
        /// </summary>
        private AssetPath(string relativePath) {
            _relativePath = relativePath!=null ? relativePath.Replace("\\", "/") : "";
        }


        /// --------------------------------------------------------------------
        #region Factory Methods

        public static AssetPath Empty() => new AssetPath("");

        public static AssetPath FromAssetPath(string assetPath) {
            if (assetPath == null) throw new ArgumentNullException(nameof(assetPath));

            if (!assetPath.StartsWith("Assets/")) {
                throw new ArgumentException("");
            }
            return new AssetPath(assetPath.Substring("Assets/".Length));
        }

        /// <summary> 
        /// "Assets/"以下の相対パスを指定して生成する
        /// </summary>
        public static AssetPath FromRelativePath(string relativePath) {
            if (relativePath == null) throw new ArgumentNullException(nameof(relativePath));

            if (relativePath.StartsWith("Assets/")) {
                relativePath = relativePath.Substring("Assets/".Length);
            }
            return new AssetPath(relativePath);
        }

        /// <summary> 
        /// "Assets/"以下の相対パスを指定して生成する
        /// </summary>
        public static AssetPath FromRelativePath(string relativeDirectoryPath, string assetName) {
            if (relativeDirectoryPath == null) throw new ArgumentNullException(nameof(relativeDirectoryPath));
            if (assetName == null) throw new ArgumentNullException(nameof(assetName));

            if (relativeDirectoryPath.StartsWith("Assets/")) {
                relativeDirectoryPath = relativeDirectoryPath.Substring("Assets/".Length);
            }
            return new AssetPath($"{relativeDirectoryPath}/{assetName}");
        }

        /// <summary>
        /// 絶対パスから生成する
        /// </summary>
        public static AssetPath FromAbsolutePath(string absolutePath) {
            string relativePath = absolutePath.Replace(Application.dataPath, "").TrimStart('\\', '/');
            return new AssetPath(relativePath);
        }

        /// <summary>
        /// 任意のアセットから生成する
        /// </summary>
        public static AssetPath FromAsset(UnityEngine.Object asset) {
            if (asset == null) throw new ArgumentNullException(nameof(asset));

            string assetPath = "";
#if UNITY_EDITOR
            assetPath = UnityEditor.AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(assetPath)) {
                throw new ArgumentException("The provided asset is not a valid asset in the Assets folder.");
            }
#endif

            return FromRelativePath(assetPath);
        }

        #endregion


        /// --------------------------------------------------------------------
        #region Convert to string

        /// <summary>
        /// 相対パス
        /// </summary>
        public string ToRelativePath() => _relativePath;

        /// <summary>
        /// アセットパス
        /// </summary>
        public string ToProjectPath() => "Assets/" + _relativePath;

        /// <summary>
        /// 絶対パス
        /// </summary>
        public string ToAbsolutePath() => Path.GetFullPath(Path.Combine(Application.dataPath, _relativePath));
        
        public override string ToString() => this.ToProjectPath();
        #endregion


        /// --------------------------------------------------------------------

        public AssetPath ChangeExtension(string newExtension) {
            return new AssetPath(Path.ChangeExtension(_relativePath, newExtension));
        }

        public AssetPath RemoveExtension() {
            return ChangeExtension(null);
        }

        public AssetPath Combine(string additionalPath) {
            return new AssetPath(Path.Combine(_relativePath, additionalPath).Replace("\\", "/"));
        }


        /// --------------------------------------------------------------------
        #region Check

        /// <summary>
        /// ファイルが存在するか確認する
        /// </summary>
        public bool Exists() {
            return this.IsFile() || this.IsDirectory();
        }

        /// <summary>
        /// パスにファイルが存在するか確認する
        /// </summary>
        public bool IsFile() {
            return File.Exists(this.ToAbsolutePath());
        }

        /// <summary>
        /// パスにフォルダが存在するか確認する
        /// </summary>
        public bool IsDirectory() {
            return Directory.Exists(this.ToAbsolutePath());
        }

        #endregion


        /// --------------------------------------------------------------------
        #region Equality Overrides

        public override bool Equals(object obj) {
            if (obj is AssetPath other) {
                return string.Equals(_relativePath, other._relativePath, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode() => _relativePath.GetHashCode(StringComparison.OrdinalIgnoreCase);

        public static bool operator ==(AssetPath left, AssetPath right) => Equals(left, right);
        
        public static bool operator !=(AssetPath left, AssetPath right) => !Equals(left, right);
        #endregion
    }
}


/// --------------------------------------------------------------------
#if UNITY_EDITOR
namespace Nitou.EditorScripts {
    using UnityEditor;

    [CustomPropertyDrawer(typeof(AssetPath))]
    public class AssetPathDrawer : PropertyDrawer {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // "relativePath"プロパティを正しく取得する
            var relativePathProperty = property.FindPropertyRelative("_relativePath");

            // "Assets/" のラベルを表示
            EditorGUI.LabelField(new Rect(position.x, position.y, 50, position.height), "Assets/");

            // 相対パスの入力フィールドを表示
            EditorGUI.PropertyField(
                new Rect(position.x + 50, position.y, position.width - 50, position.height),
                relativePathProperty,
                GUIContent.none
            );
        }
    }
}
#endif