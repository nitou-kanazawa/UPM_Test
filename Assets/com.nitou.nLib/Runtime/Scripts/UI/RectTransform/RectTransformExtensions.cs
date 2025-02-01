using UnityEngine;
using UnityEngine.UI;

// [REF]
//  ねこじゃらシティ: RectTransformのサイズをスクリプトから変更する https://nekojara.city/unity-rect-transform-size
//  _: nGUIなどのRectTransformのwidthやheightなどの値を変更する方法 https://nekosuko.jp/1792/
//  Unity Forums: Best algorithm to clamp a UI window within the canvas? https://forum.unity.com/threads/best-algorithm-to-clamp-a-ui-window-within-the-canvas.314034/
//  Hatena: RectTransformのスクリーン座標のRectを取得する https://hacchi-man.hatenablog.com/entry/2020/12/11/220000

namespace Nitou {

    /// <summary>
    /// <see cref="RectTransform"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static partial class RectTransformExtensions {

        // [NOTE] RectTransform.GetCornersは0:左下、1左上、2:右上、3:右下の順で点が格納される

        // 計算用
        private static readonly Vector3[] _corners = new Vector3[4];
        private static readonly Vector3[] _corners2 = new Vector3[4];

        // 定数
        private const int CORNER_COUNT = 4;

        /// <summary>
        /// Rectの各コーナー．
        /// </summary>
        public enum Corner {
            Min = 0,
            MinX_MaxY = 1,
            Max = 2,
            MaxX_MinY = 3
        }


        // ----------------------------------------------------------------------------
        #region WORLD座標

        // Getter

        /// <summary>
        /// ワールド座標でのコーナー位置を取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldPosition(this RectTransform self, Corner corner = Corner.Min) {
            self.GetWorldCorners(_corners);

            return _corners[(int)corner];  // ※Zは無視
        }

        /// <summary>
        /// ワールド座標での中心位置を取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldCenterPosition(this RectTransform self) {
            self.GetWorldCorners(_corners);

            return _corners.GetCenter();
        }

        /// <summary>
        /// ワールド座標でのサイズを取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldSize(this RectTransform self) {
            self.GetWorldCorners(_corners);

            var min = _corners[(int)Corner.Min];
            var max = _corners[(int)Corner.Max];
            return max - min;
        }

        /// <summary>
        /// ワールド座標での位置とサイズを取得する拡張メソッド．
        /// </summary>
        public static Rect GetWorldRect(this RectTransform self) {
            self.GetWorldCorners(_corners);

            var min = _corners[(int)Corner.Min];
            var max = _corners[(int)Corner.Max];
            return new Rect(min, max - min);
        }

        // Setter

        /// <summary>
        /// ワールド座標でのコーナー位置を設定する拡張メソッド．
        /// </summary>
        public static void SetWorldPosition(this RectTransform self, Vector3 worldPos, Corner corner = Corner.Min) {
            // 現在のワールド座標
            var currentWorldPos = self.GetWorldPosition(corner);

            // 位置の差分を計算し、ローカル座標に反映
            var delta = worldPos - currentWorldPos;
            self.localPosition += delta;
        }

        /// <summary>
        /// ワールド座標でのコーナー位置を設定する拡張メソッド．
        /// </summary>
        public static void SetWorldCenterPosition(this RectTransform self, Vector3 worldPos) {
            // 現在のワールド座標
            var currentWorldPos = self.GetWorldCenterPosition();

            // 位置の差分を計算し、ローカル座標に反映
            var delta = worldPos - currentWorldPos;
            self.localPosition += delta;
        }
        #endregion


        // ----------------------------------------------------------------------------
        #region SCREEN座標

        // [REF]
        //  テラシュール: Screenの座標とWorld（3D）座標の変換について https://tsubakit1.hateblo.jp/entry/2016/03/01/020510
        //  LIGHT11: uGUIにアルファ付きのマスクを掛ける https://light11.hatenadiary.com/entry/2019/04/24/232041

        // Getter

        /// <summary>
        /// スクリーン座標でのコーナー位置を取得する拡張メソッド．
        /// </summary>
        public static Vector2 GetScreenPosition(this RectTransform self, ref Canvas canvas, Corner corner = Corner.Min) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Vector2.zero;
            }

            // ワールド座標 → スクリーン座標
            var worldPos = self.GetWorldPosition(corner);
            return canvas.GetScreenPosition(worldPos);
        }

        /// <summary>
        /// スクリーン座標でのコーナー位置を取得する拡張メソッド．
        /// （Canvasをキャッシュしない場合）
        /// </summary>
        public static Vector2 GetScreenPosition(this RectTransform self, Corner corner = Corner.Min) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバス取得
            var canvas = self.GetBelongedCanvas();
            if (canvas == null) {
                Debug_.LogWarning("Root Canvas does not exist. Please ensure the UI element is placed under a Canvas.");
                return Vector2.zero;
            }

            // ワールド座標→スクリーン座標
            var worldPos = self.GetWorldPosition(corner);
            return canvas.GetScreenPosition(worldPos);
        }


        /// <summary>
        /// ワールド座標での中心位置を取得する拡張メソッド．
        /// </summary>
        public static Vector2 GetScreenCenterPosition(this RectTransform self, ref Canvas canvas) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Vector2.zero;
            }

            // ワールド座標→スクリーン座標
            var worldCenter = self.GetWorldCenterPosition();
            return canvas.GetScreenPosition(worldCenter);
        }

        /// <summary>
        /// ワールド座標での中心位置を取得する拡張メソッド．
        /// （Canvasをキャッシュしない場合）
        /// </summary>
        public static Vector2 GetScreenCenterPosition(this RectTransform self) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバス取得
            Canvas canvas = null;
            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Vector2.zero;
            }

            // ワールド座標→スクリーン座標
            var worldCenter = self.GetWorldCenterPosition();
            return canvas.GetScreenPosition(worldCenter);
        }


        /// <summary>
        /// スクリーン座標での位置を取得する拡張メソッド．
        /// </summary>
        public static Rect GetScreenRect(this RectTransform self, ref Canvas canvas) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバスの取得
            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Rect.zero;
            }

            // ワールド座標→スクリーン座標
            var worldMin = self.GetWorldPosition(Corner.Min);
            var worldMax = self.GetWorldPosition(Corner.Max);
            return canvas.GetScreenRect(worldMin, worldMax);
        }

        /// <summary>
        /// スクリーン座標での位置を取得する拡張メソッド．
        /// </summary>
        public static Rect GetScreenRect(this RectTransform self) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバスの取得
            Canvas canvas = null;
            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Rect.zero;
            }

            // ワールド座標→スクリーン座標
            var worldMin = self.GetWorldPosition(Corner.Min);
            var worldMax = self.GetWorldPosition(Corner.Max);
            return canvas.GetScreenRect(worldMin, worldMax);
        }

        // Setter

        /*

        /// <summary>
        /// スクリーン座標でコーナー位置を設定する．
        /// </summary>
        public static void SetScreenPosition(this RectTransform self, Vector2 screenPos, ref Canvas canvas, Corner corner = Corner.Min) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバスの取得
            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return;
            }

            // スクリーン座標→ワールド座標の変換
            Vector3 worldPos = ScreenToWorldPosition(screenPos, canvas);

            // ワールド座標をローカル座標に変換し、RectTransformに反映
            SetWorldPosition(self, worldPos, corner);
        }


        /// <summary>
        /// スクリーン座標をワールド座標に変換する内部メソッド．
        /// </summary>
        private static Vector3 ScreenToWorldPosition(Vector2 screenPos, Canvas canvas) {
            Vector3 worldPos = Vector3.zero;

            switch (canvas.renderMode) {
                case RenderMode.ScreenSpaceOverlay:
                    worldPos = screenPos;
                    break;

                case RenderMode.ScreenSpaceCamera:
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out worldPos);
                    break;

                case RenderMode.WorldSpace:
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(canvas.transform as RectTransform, screenPos, Camera.main, out worldPos);
                    break;

                default:
                    throw new System.NotImplementedException();
            }
            return worldPos;
        }
        */
        #endregion


        // ----------------------------------------------------------------------------
        #region VIEW PORT座標

        /// <summary>
        /// ビューポート座標系での位置を取得する拡張メソッド．
        /// </summary>
        public static Vector2 GetViewportPos(this RectTransform self, ref Canvas canvas, Corner corner = Corner.Min) {

            // スクリーン座標
            var screenPos = self.GetScreenPosition(ref canvas, corner);

            // ビューポート座標
            return new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
        }

        /// <summary>
        /// ビューポート座標系での位置とサイズを取得する拡張メソッド．
        /// </summary>
        public static Rect GetViewportRect(this RectTransform self, ref Canvas canvas, Camera camera = null) {
            if (self == null) throw new System.ArgumentNullException(nameof(self));

            // キャンバスの取得
            if (!self.TryGetBelongedCanvasIfNull(ref canvas)) {
                return Rect.zero;
            }

            // ワールド座標 → ビューポート座標
            var worldMin = self.GetWorldPosition(Corner.Min);
            var worldMax = self.GetWorldPosition(Corner.Max);
            return canvas.GetViewportRect(worldMin, worldMax);

            //// スクリーン座標
            //var screenRect = self.GetScreenRect(ref canvas);

            //Camera.main.WorldToViewportPoint

            //// ビューポート座標
            //Debug_.Log($"[w:{screenRect.width}, h:{screenRect.height}] / [w:{Screen.width}, h:{Screen.height}]");
            //var viewportMin = new Vector2(screenRect.min.x / Screen.width, screenRect.min.y / Screen.height);
            //var viewportSize = new Vector2(screenRect.size.x / Screen.width, screenRect.size.y / Screen.height);
            //return new Rect(viewportMin, viewportSize);
        }
        #endregion


        // ----------------------------------------------------------------------------
        #region RELATIVE座標

        // [NOTE] RectUtilsに処理を移行
 
        #endregion


        /// <summary>
        /// 
        /// </summary>
        private static Vector3 GetCenter(this Vector3[] corners) {
            return (corners[(int)Corner.Min] + corners[(int)Corner.Max]) / 2f;
        }


        // ----------------------------------------------------------------------------
        #region コンポーネント取得

        /// <summary>
        /// 親階層をたどって所属する<see cref="Canvas"/>を取得する拡張メソッド．
        /// </summary>
        public static Canvas GetBelongedCanvas(this RectTransform self) {
            var currentTrans = self.transform;
            while (currentTrans != null) {
                if (currentTrans.TryGetComponent<Canvas>(out var canvas)) {
                    return canvas;
                }
                currentTrans = currentTrans.parent;
            }
            return null;
        }

        /// <summary>
        /// 親階層をたどって所属する<see cref="CanvasScaler"/>を取得する拡張メソッド．
        /// </summary>
        public static CanvasScaler GetBelongedCanvasScaler(this RectTransform self) {
            var canvas = self.GetBelongedCanvas();
            if (canvas is null) return null;

            return canvas.GetComponent<CanvasScaler>();
        }

        /// <summary>
        /// 親階層をたどって所属する<see cref="CanvasScaler"/>を取得する拡張メソッド．
        /// </summary>
        private static bool TryGetBelongedCanvasIfNull(this RectTransform self, ref Canvas canvas) {
            if (canvas == null) {
                canvas = self.GetBelongedCanvas();

                if (canvas == null) {
                    Debug_.LogWarning("Root Canvas does not exist. Please ensure the UI element is placed under a Canvas.");
                    return false;
                }
            }
            return true;
        }
        #endregion


        // ----------------------------------------------------------------------------
        #region 重なり判定

        // [参考]
        //  kanのメモ帳: RectTransformが重なっているか(衝突しているか)をコライダーを使わないで判定する拡張メソッド https://kan-kikuchi.hatenablog.com/entry/RectTransform_IsOverlapping
        //  Hatena: 特定のRectTransformの範囲内かどうか判定する https://hacchi-man.hatenablog.com/entry/2020/05/09/220000

        /// <summary>
        /// 他の<see cref="RectTransform"/>と重なっているか判定する拡張メソッド．
        /// </summary>
        public static bool Overlaps(this RectTransform self, RectTransform othrer) {

            // コーナー座標を取得
            self.GetWorldCorners(_corners);
            othrer.GetWorldCorners(_corners2);

            // 各コーナーをチェック
            for (var i = 0; i < CORNER_COUNT; i++) {

                //rect1の角がrect2の内部にあるか
                if (IsPointInsideRect(_corners[i], _corners2)) {
                    return true;
                }
                //rect2の角がrect1の内部にあるか
                if (IsPointInsideRect(_corners2[i], _corners)) {
                    return true;
                }
            }

            return false;
        }

        // [参考]
        //  _: 点の多角形に対する内外判定 https://www.nttpc.co.jp/technology/number_algorithm.html


        /// 点が指定されたRect内に存在するかどうかを判定する
        private static bool IsPointInsideRect(Vector2 point, Vector3[] rectCorners) {
            var inside = false;

            //rectCornersの各頂点に対して、pointがrect内にあるかを確認
            for (int i = 0, j = 3; i < CORNER_COUNT; j = i++) {

                // 各コーナーのy座標がpointのy座標と比較してどちらに位置するか
                bool pointIsBetweenYOfCurrentAndPreviousCorners =
                    (rectCorners[i].y > point.y) != (rectCorners[j].y > point.y);

                // point.x の位置と、i と j のコーナー間の直線上の x 座標を比較
                float intersectionX = rectCorners[i].x +
                    (rectCorners[j].x - rectCorners[i].x) * (point.y - rectCorners[i].y) /
                    (rectCorners[j].y - rectCorners[i].y);

                // 説明変数: point.x が、現在のコーナー間の直線を横切るかどうかを確認
                bool pointIsLeftOfIntersection = point.x < intersectionX;

                // 状態の更新
                if (pointIsBetweenYOfCurrentAndPreviousCorners && pointIsLeftOfIntersection) {
                    inside = !inside;
                }
            }

            return inside;
        }

        public static bool Contains(this RectTransform self, RectTransform other) {
            var selfBounds = GetBounds(self);
            var otherBounds = GetBounds(other);

            return selfBounds.Contains(new Vector3(otherBounds.min.x, otherBounds.min.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.max.x, otherBounds.max.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.min.x, otherBounds.max.y, 0f)) &&
                   selfBounds.Contains(new Vector3(otherBounds.max.x, otherBounds.min.y, 0f));
        }


        /// <summary>
        /// 計算用の<see cref="Bounds"/>を取得する．
        /// </summary>
        private static Bounds GetBounds(this RectTransform self) {
            var min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            var max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // 最小、最大の取得
            self.GetWorldCorners(_corners);
            for (var i = 0; i < CORNER_COUNT; i++) {
                min = Vector3.Min(_corners[i], min);
                max = Vector3.Max(_corners[i], max);
            }

            max.z = 0f;
            min.z = 0f;

            // AABBを定義
            var bounds = new Bounds(min, Vector3.zero);
            bounds.Encapsulate(max);
            return bounds;
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region その他

        // サイズ設定

        /// <summary>
        /// Widthを設定する拡張メソッド．
        /// </summary>
        public static void SetWidth(this RectTransform self, float width) {
            self.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        /// <summary>
        /// Heightを設定する拡張メソッド．
        /// </summary>
        public static void SetHeight(this RectTransform self, float height) {
            self.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        /// <summary>
        /// サイズを指定する拡張メソッド．
        /// </summary>
        public static void SetSize(this RectTransform self, Vector2 size) {

            // 親要素のサイズ取得
            var parent = self.parent as RectTransform;
            var parentSize = parent != null ? parent.rect.size : Vector2.zero;

            // 自身のアンカーサイズを計算
            var anchorSize = parentSize * (self.anchorMax - self.anchorMin);

            // 入力サイズからアンカーサイズを引いた結果が
            // sizeDeltaに指定すべき値
            self.sizeDelta = size - anchorSize;
        }

        // ピボット設定

        /// <summary>
        /// ピボットXを設定する拡張メソッド．
        /// </summary>
        public static void SetPivotX(this RectTransform self, float x) {
            self.pivot = new Vector2(Mathf.Clamp01(x), self.pivot.y);
        }

        /// <summary>
        /// ピボットYを設定する拡張メソッド．
        /// </summary>
        public static void SetPivotY(this RectTransform self, float y) {
            self.pivot = new Vector2(self.pivot.x, Mathf.Clamp01(y));
        }

        #endregion



        // [REF] UniDiscussions: Test if UI element is visible on screen https://discussions.unity.com/t/test-if-ui-element-is-visible-on-screen/554949/16

        /// <summary>
        /// Counts the bounding box corners of the given RectTransform that are visible in screen space.
        /// </summary>
        /// <returns>The amount of bounding box corners that are visible.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera. Leave it null for Overlay Canvasses.</param>
        private static int CountCornersVisibleFrom(this RectTransform rectTransform, Camera camera = null) {
            Rect screenBounds = new Rect(0f, 0f, Screen.width, Screen.height); // Screen space bounds (assumes camera renders across the entire screen)
            Vector3[] objectCorners = new Vector3[4];
            rectTransform.GetWorldCorners(objectCorners);

            int visibleCorners = 0;
            Vector3 tempScreenSpaceCorner; // Cached
            for (var i = 0; i < objectCorners.Length; i++) // For each corner in rectTransform
            {
                if (camera != null)
                    tempScreenSpaceCorner = camera.WorldToScreenPoint(objectCorners[i]); // Transform world space position of corner to screen space
                else {
                    Debug.Log(rectTransform.gameObject.name + " :: " + objectCorners[i].ToString("F2"));
                    tempScreenSpaceCorner = objectCorners[i]; // If no camera is provided we assume the canvas is Overlay and world space == screen space
                }

                if (screenBounds.Contains(tempScreenSpaceCorner)) // If the corner is inside the screen
                {
                    visibleCorners++;
                }
            }
            return visibleCorners;
        }

        /// <summary>
        /// Determines if this RectTransform is fully visible.
        /// Works by checking if each bounding box corner of this RectTransform is inside the screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is fully visible; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera. Leave it null for Overlay Canvasses.</param>
        public static bool IsFullyVisibleFrom(this RectTransform rectTransform, Camera camera = null) {
            if (!rectTransform.gameObject.activeInHierarchy)
                return false;

            return CountCornersVisibleFrom(rectTransform, camera) == 4; // True if all 4 corners are visible
        }

        /// <summary>
        /// Determines if this RectTransform is at least partially visible.
        /// Works by checking if any bounding box corner of this RectTransform is inside the screen space view frustrum.
        /// </summary>
        /// <returns><c>true</c> if is at least partially visible; otherwise, <c>false</c>.</returns>
        /// <param name="rectTransform">Rect transform.</param>
        /// <param name="camera">Camera. Leave it null for Overlay Canvasses.</param>
        public static bool IsVisibleFrom(this RectTransform rectTransform, Camera camera = null) {
            if (!rectTransform.gameObject.activeInHierarchy)
                return false;

            return CountCornersVisibleFrom(rectTransform, camera) > 0; // True if any corners are visible
        }

    }


}
