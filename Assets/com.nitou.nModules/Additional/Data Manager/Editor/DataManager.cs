using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector.Editor;

// [REF]
//  youtube: Data Manager - Scriptable Object Editor Window https://www.youtube.com/watch?v=1zu41Ku46xU&t=23s

namespace Nitou.DataManagement {

    /// <summary>
    /// "ManageableDataAttribute"を付与したScriptableObjectを編集するエディタウインドウ
    /// </summary>
    internal class DataManager : OdinMenuEditorWindow {

        // 対象の型情報
        private static Type[] typesToDisplay = TypeCache.GetTypesWithAttribute<ManageableDataAttribute>()
            .OrderBy(m => m.Name)
            .ToArray();

        // 選択中の型
        private Type selectedType;


        /// ----------------------------------------------------------------------------
        // OdinEditor Method 

        /// <summary>
        /// ウインドウの表示
        /// </summary>
        [MenuItem("Tools/Open/Data Manager")]
        private static void OpenEditor() => GetWindow<DataManager>();

        /// 
        protected override void OnImGUI() {

            if (typesToDisplay.Length!=0) {

                // 型選択ボタンリストの表示
                if (GUIUtils.SelectButonList(ref selectedType, typesToDisplay))
                    this.ForceMenuTreeRebuild();    // ※ボタン押下されたら再描画
            }

            base.OnImGUI();
        }



        /// <summary>
        /// メニュー画面の構築
        /// </summary>
        /// <returns></returns>
        protected override OdinMenuTree BuildMenuTree() {

            var tree = new OdinMenuTree();
            tree.AddAllAssetsAtPath(selectedType.Name, "Assets/", selectedType, true, true);
            return tree;
        }

    }
}