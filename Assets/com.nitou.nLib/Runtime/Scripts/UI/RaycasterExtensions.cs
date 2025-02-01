using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Nitou{
    using Nitou.DesignPattern.Pooling;

    /// <summary>
    /// Raycaster関連の基本的な拡張メソッド集
    /// </summary>
    public static class RaycasterExtensions {

        /// <summary>
        /// Raycastの拡張メソッド．
        /// （毎回リストを生成するため、基本的には引数にリストを受け取る通常版を使用する．）
        /// </summary>
        public static List<RaycastResult> Raycast(this BaseRaycaster self, PointerEventData pointerEventData) {
            List<RaycastResult> results = new();
            self.Raycast(pointerEventData, results);
            return results;
        }

        /// <summary>
        /// 指定したスクリーン座標にUIがあるかどうか調べる
        /// </summary>
        public static bool OverlapUI(this IEnumerable<GraphicRaycaster> raycasters, PointerEventData pointerEventData) {

            var results = ListPool<RaycastResult>.New();
            bool isOverlap = false;

            try {
                // 各Raycasterで重なり判定を行う
                foreach (var raycaster in raycasters.WithoutNull()) {
                    raycaster.Raycast(pointerEventData, results);
                    if (results.Count > 0) {
                        isOverlap = true;
                        break;
                    }
                }
            } finally {
                results.Free();
            }
            return isOverlap;
        }

        /// <summary>
        /// 指定したRaycasterの中から、重なりのあるコンポーネントを取得する
        /// </summary>
        public static T GetOverlapComponent<T>(this IEnumerable<BaseRaycaster> raycasters, PointerEventData pointerEventData)
            where T : class {
            
            var results = ListPool<RaycastResult>.New();
            T target = default;

            try {
                foreach (var raycaster in raycasters.WithoutNull()) {
                    raycaster.Raycast(pointerEventData, results);
                    if (results.IsEmpty()) continue;

                    // 最初に見つかった重なりのあるコンポーネントを取得
                    target = results.FirstOrDefault(r => r.gameObject.GetComponent<T>() != null).gameObject?.GetComponent<T>();
                    if (target != null) break; // コンポーネントが見つかったらループを終了
                }
            } finally {
                results.Free();
            }

            return target; // 見つからなかった場合はデフォルト値を返す
        }

        /// <summary>
        /// GraphicRaycasterのリストからDragコンポーネントを取得します。
        /// </summary>
        public static T GetDragAtPosition<T>(this IEnumerable<BaseRaycaster> raycasters, PointerEventData pointerEventData) {
            return raycasters
                .Where(raycaster => raycaster != null)
                .SelectMany(raycaster => {
                    var results = new List<RaycastResult>();
                    raycaster.Raycast(pointerEventData, results);
                    return results;
                })
                .Select(result => result.gameObject.GetComponentInParent<T>())
                .FirstOrDefault(drag => drag != null);
        }



        /// <summary>
        /// <see cref="GraphicRaycaster"/>をCanvasのsortingOrder順にソートする
        /// </summary>
        public static IEnumerable<GraphicRaycaster> OrderBySortingOrder(this IEnumerable<GraphicRaycaster> raycasters) {
            return raycasters
                // SortingOrderで昇順にソート
                .OrderBy(raycaster =>  raycaster.sortOrderPriority);
        }
    }


    /// <summary>
    /// <see cref="RaycastResult"/>型の基本的な拡張メソッド集
    /// </summary>
    public static class RaycastResultExtensions {

    }


}
