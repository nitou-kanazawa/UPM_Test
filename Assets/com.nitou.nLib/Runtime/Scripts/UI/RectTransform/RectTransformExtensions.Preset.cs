using UnityEngine;

// [参考]
//  ねこじゃらシティ: RectTransform.sizeDeltaの仕様と注意点 https://nekojara.city/unity-rect-transform-size-delta
//  github: neon-izm/AnchorPreset.cs https://gist.github.com/neon-izm/512a439fe6d07348f6f421c6061338e3

namespace Nitou {

    // [NOTE]
    //  ・アンカー4点を同じ位置にまとめると、サイズは固定される（固定サイズ）
    //  　→ (sizeDelta = 要素のサイズ)
    //  ・アンカー4点をバラけさせると、サイズはParentに対して相対的に決まる（可変サイズ）
    //  　→ (sizeDelta = 要素のサイズ – 親要素のサイズ)

    /// <summary>
    /// アンカーの種類
    /// </summary>
    public enum AnchorPresets {
        // ----- 固定サイズ -----
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
        BottomStretch,

        // ----- 可変サイズ -----
        VertStretchLeft,
        VertStretchRight,
        VertStretchCenter,

        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,

        StretchAll
    }

    /// <summary>
    /// ピボットの種類
    /// </summary>
    public enum PivotPresets {
        // Top
        TopLeft,
        TopCenter,
        TopRight,

        // Middle
        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        // Bottom
        BottomLeft,
        BottomCenter,
        BottomRight,
    }


    /// <summary>
    /// <see cref="RectTransform"/>型の基本的な拡張メソッド集
    /// </summary>
    public static partial class RectTransformExtensions {

        /// <summary>
        /// 固定サイズ(※anchorMinとanchorMaxが同じ位置)かどうか確認する拡張メソッド
        /// </summary>
        public static bool IsFixedSize(this RectTransform self) {
            return self.anchorMin == self.anchorMax;
        }


        /// ----------------------------------------------------------------------------
        // アンカー、ピボットの設定

        /// <summary>
        /// アンカーを設定する拡張メソッド
        /// </summary>
        public static void SetAnchor(this RectTransform self, AnchorPresets allign, int offsetX = 0, int offsetY = 0) {
            // 
            self.anchoredPosition = new Vector3(offsetX, offsetY, 0);

            // Allign更新
            switch (allign) {
                case (AnchorPresets.TopLeft): {
                        self.anchorMin = new Vector2(0, 1);
                        self.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.TopCenter): {
                        self.anchorMin = new Vector2(0.5f, 1);
                        self.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.TopRight): {
                        self.anchorMin = new Vector2(1, 1);
                        self.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.MiddleLeft): {
                        self.anchorMin = new Vector2(0, 0.5f);
                        self.anchorMax = new Vector2(0, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleCenter): {
                        self.anchorMin = new Vector2(0.5f, 0.5f);
                        self.anchorMax = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleRight): {
                        self.anchorMin = new Vector2(1, 0.5f);
                        self.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }

                case (AnchorPresets.BottomLeft): {
                        self.anchorMin = new Vector2(0, 0);
                        self.anchorMax = new Vector2(0, 0);
                        break;
                    }
                case (AnchorPresets.BottomCenter): {
                        self.anchorMin = new Vector2(0.5f, 0);
                        self.anchorMax = new Vector2(0.5f, 0);
                        break;
                    }
                case (AnchorPresets.BottomRight): {
                        self.anchorMin = new Vector2(1, 0);
                        self.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.HorStretchTop): {
                        self.anchorMin = new Vector2(0, 1);
                        self.anchorMax = new Vector2(1, 1);
                        break;
                    }
                case (AnchorPresets.HorStretchMiddle): {
                        self.anchorMin = new Vector2(0, 0.5f);
                        self.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }
                case (AnchorPresets.HorStretchBottom): {
                        self.anchorMin = new Vector2(0, 0);
                        self.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.VertStretchLeft): {
                        self.anchorMin = new Vector2(0, 0);
                        self.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchCenter): {
                        self.anchorMin = new Vector2(0.5f, 0);
                        self.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchRight): {
                        self.anchorMin = new Vector2(1, 0);
                        self.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.StretchAll): {
                        self.anchorMin = new Vector2(0, 0);
                        self.anchorMax = new Vector2(1, 1);
                        break;
                    }
            }
            
        }

        /// <summary>
        /// アンカーを設定する拡張メソッド
        /// </summary>
        public static void SetAnchor(this RectTransform self, AnchorPresets allign, Vector2Int offset) =>
            self.SetAnchor(allign, offset.x, offset.y);

        /// <summary>
        /// ピボットを設定する拡張メソッド
        /// </summary>
        public static void SetPivot(this RectTransform self, PivotPresets preset) {

            switch (preset) {
                case (PivotPresets.TopLeft): {
                        self.pivot = new Vector2(0, 1);
                        break;
                    }
                case (PivotPresets.TopCenter): {
                        self.pivot = new Vector2(0.5f, 1);
                        break;
                    }
                case (PivotPresets.TopRight): {
                        self.pivot = new Vector2(1, 1);
                        break;
                    }

                case (PivotPresets.MiddleLeft): {
                        self.pivot = new Vector2(0, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleCenter): {
                        self.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleRight): {
                        self.pivot = new Vector2(1, 0.5f);
                        break;
                    }

                case (PivotPresets.BottomLeft): {
                        self.pivot = new Vector2(0, 0);
                        break;
                    }
                case (PivotPresets.BottomCenter): {
                        self.pivot = new Vector2(0.5f, 0);
                        break;
                    }
                case (PivotPresets.BottomRight): {
                        self.pivot = new Vector2(1, 0);
                        break;
                    }
            }
        }


        /// ----------------------------------------------------------------------------
        // Rectの設定

        /// <summary>
        /// Parentの各端点から距離を指定してサイズ設定を行う拡張メソッド
        /// </summary>
        public static void SetRectBasedOnParentEdiges(this RectTransform self,
            float top, float bottom, float right, float left
        ) {

            // 可変サイズ設定
            self.SetAnchor(AnchorPresets.StretchAll);

            // 親要素の各辺からの相対的な位置を設定する
            self.offsetMin = new Vector2(left, bottom);
            self.offsetMax = new Vector2(-right, -top);
        }

        /// <summary>
        /// Parentの各端点から距離を指定してサイズ設定を行う拡張メソッド
        /// </summary>
        public static void SetRectBasedOnParentEdiges(this RectTransform self, Padding padding) =>
            self.SetRectBasedOnParentEdiges(padding.top, padding.bottom, padding.right, padding.left);

    }
}