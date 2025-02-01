using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

// [参考]
// Hatena: EventSystemsから受け取った座標をRectTransform.localPositionに設定する方法 https://appleorbit.hatenablog.com/entry/2015/10/23/000403

namespace Nitou {

    /// <summary>
    /// <see cref="PointerEventData"/>の拡張メソッドクラス
    /// </summary>
    public static class PointerEventDataExtensitions {

        /// ----------------------------------------------------------------------------
        // 座標の取得

        /// <summary>
        /// 座標を取得する拡張メソッド
        /// ※CanvasのRenderMode.WorldSpaceは非対応
        /// </summary>
        public static Vector2 GetScreenSpaceLocalPosition(this PointerEventData self, RectTransform parentRect) {
            var screenPosition = self.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                screenPosition,
                self.pressEventCamera,
                out var result
            );
            return result;
        }

        /// <summary>
        /// 座標を取得する拡張メソッド
        /// ※CanvasのRenderMode.WorldSpaceは非対応
        /// </summary>
        public static Vector2 GetScreenSpacePosition(this PointerEventData self, RectTransform parentRect) {
            var screenPosition = self.position;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                parentRect,
                screenPosition,
                self.pressEventCamera,
                out var result
            );
            return result;
        }


        /// ----------------------------------------------------------------------------
        // 

        /// <summary>
        /// イベント発生地点のDropdownAreaを取得する拡張メソッド
        /// </summary>
        public static bool TryGetRaycastArea<T>(this PointerEventData self, out T comonent) 
            where T : Component {

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(self, results);

            // ※最も上にあるものを取得する
            comonent = results
                .Select(x => x.gameObject.GetComponent<T>())
                .FirstOrDefault(x => x != null);

            return comonent != null;
        }
    }
}