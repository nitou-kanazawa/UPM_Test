#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Nitou.Tools.Hierarchy {

    internal static class HierarchyObjectCreationMenu {

        /// <summary>
        /// ヘッダーの生成
        /// </summary>
        [MenuItem(
            GameObjectMenu.Prefix.DammyObject + "Header", 
            priority = GameObjectMenu.Order.DammyObject
        )]
        static void CreateHeader(MenuCommand menuCommand) {

            var obj = new GameObject("Header");
            obj.AddComponent<HierarchyHeader>();
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
        }

        /// <summary>
        /// フォルダの生成
        /// </summary>
        [MenuItem(
            GameObjectMenu.Prefix.DammyObject + "Folder",
            priority = GameObjectMenu.Order.DammyObject
        )]
        static void CreateFolder(MenuCommand menuCommand) {

            var obj = new GameObject("Folder");
            obj.AddComponent<HierarchyFolder>();
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
        }

        /// <summary>
        /// セパレータの生成
        /// </summary>
        [MenuItem(
            GameObjectMenu.Prefix.DammyObject + "Separator",
            priority = GameObjectMenu.Order.DammyObject
        )]
        static void CreateSeparator(MenuCommand menuCommand) {

            var obj = new GameObject("Separator");
            obj.AddComponent<HierarchySeparator>();
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);

            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
        }        
    }
}
#endif