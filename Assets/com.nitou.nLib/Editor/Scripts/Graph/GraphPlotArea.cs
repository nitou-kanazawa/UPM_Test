#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nitou.EditorShared {
    public partial class GUIGraph {

        /// <summary>
        /// グラフのプロット領域
        /// </summary>
        public class GraphPlotArea {

            public enum PointType {
                GraphPoint,
                GUIPoint,
            }

            // 描画域
            public readonly Rect rect;

            // 各軸の値域
            public GraphAxis xAxis;
            public GraphAxis yAxis;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public GraphPlotArea(Rect rect, GraphAxis xAxis, GraphAxis yAxis) {
                this.rect = rect;
                this.xAxis = xAxis;
                this.yAxis = yAxis;
            }


            /// ----------------------------------------------------------------------------
            // Public Method (Drawing)

            /// <summary>
            /// 背景と枠線を描画する
            /// </summary>
            public void DrawBackDrop(Color backdropColor, Color outlineColor) {
                Handles.DrawSolidRectangleWithOutline(rect, backdropColor, outlineColor);
            }

            /// <summary>
            /// X軸のグリッドを描画する
            /// </summary>
            public void DrawGridX(Color color) {
                using (new Handles.DrawingScope(color)) {
                    var start = Mathf.Ceil(xAxis.range.Min / xAxis.step) * xAxis.step;
                    var ticks = EnumerableUtils.LinspaceWithStep(start, xAxis.range.Max, xAxis.step);

                    ticks.ForEach(x => {
                        DrawLine(new Vector2(x, yAxis.range.Min), new Vector2(x, yAxis.range.Max), PointType.GUIPoint);
                        if (xAxis.showTicks) {
                            DrawTickLabelX(x);
                        }
                    });
                }
            }

            /// <summary>
            /// Y軸のグリッドを描画する
            /// </summary>
            public void DrawGridY(Color color) {
                using (new Handles.DrawingScope(color)) {
                    var start = Mathf.Ceil(yAxis.range.Min / yAxis.step) * yAxis.step;
                    var ticks = EnumerableUtils.LinspaceWithStep(start, yAxis.range.Max, yAxis.step);

                    ticks.ForEach(y => {
                        DrawLine(new Vector2(xAxis.range.Min, y), new Vector2(xAxis.range.Max, y), PointType.GUIPoint);
                        if (yAxis.showTicks) {
                            DrawTickLabelY(y);
                        }
                    });
                }
            }

            /// <summary>
            /// 点列を描画する
            /// </summary>
            public void DrawData(Vector2[] values, Color color, float thickness = 2f) {
                using (new GUI.ClipScope(rect))
                using (new Handles.DrawingScope(color)) {
                    DrawLines(values, PointType.GraphPoint);
                }
            }


            /// ----------------------------------------------------------------------------
            // Private Method (Drawing)

            private void DrawPoint(Vector2 value, PointType type, float radius = 3f) {
                var p = GetPoint(value, type);
                Handles.DrawSolidDisc(p, Vector3.forward, radius);
            }

            private void DrawLine(Vector2 value1, Vector2 value2, PointType type, float thickness = 1f) {
                var p1 = GetPoint(value1, type);
                var p2 = GetPoint(value2, type);
                Handles.DrawLine(p1, p2);
            }

            private void DrawLines(Vector2[] values, PointType type, float thickness = 4f) {
                for (int i = 0; i < values.Length - 1; i++) {
                    DrawLine(values[i], values[i + 1], type, thickness);
                }
                values.ForEach(value => DrawPoint(value, type));
            }

			/// <summary>
			/// X軸の目盛りラベルを表示する
			/// </summary>
            private void DrawTickLabelX(float x) {
                if (!xAxis.range.Contains(x)) return;

                var p = GetPoint(new Vector2(x, yAxis.range.Min), PointType.GUIPoint);
                var offset = new Vector2(0, 10);
                var labelRect = RectUtils.CenterSizeRect(p + offset, new Vector2(30, 20));
                GUI.Label(labelRect, x.ToString("0.0"), Style.label);
            }

			/// <summary>
			/// Y軸の目盛りラベルを表示する
			/// </summary>
			private void DrawTickLabelY(float y) {
                if (!yAxis.range.Contains(y)) return;

                var p = GetPoint(new Vector2(xAxis.range.Min, y), PointType.GUIPoint);
                var offset = new Vector2(-20, 0);
                var labelRect = RectUtils.CenterSizeRect(p + offset, new Vector2(30, 20));
                GUI.Label(labelRect, y.ToString("0.0"), Style.label);
            }

			public void DrawAxisTitleX() {
				var axisCenter = GetPoint(new Vector2(xAxis.range.Mid, yAxis.range.Min), PointType.GUIPoint);
				var offset = new Vector2(0, 25);
				var labelRect = RectUtils.CenterSizeRect(axisCenter + offset, new Vector2(150, 20));
                GUI.Label(labelRect, xAxis.label, Style.label);
			}

			public void DrawAxisTitleY() {
				var axisCenter = GetPoint(new Vector2(xAxis.range.Min, yAxis.range.Mid), PointType.GUIPoint);
				using (new EditorUtil.RotateScope(-90, axisCenter)) {
					var offset = new Vector2(0, -40);	// ※-90度の回転を考慮したオフセット
					var labelRect = RectUtils.CenterSizeRect(axisCenter + offset, new Vector2(150, 30));
					//EditorGUI.DrawRect(labelRect, Color.blue);
                    GUI.Label(labelRect, yAxis.label, Style.label);
                }
			}


            /// ----------------------------------------------------------------------------
            // Private Method

            private Vector2 GetPoint(Vector2 value, PointType type) => type switch {
                PointType.GraphPoint => ValueToGraphPoint(value),
                PointType.GUIPoint => ValueToGUIPoint(value),
                _ => throw new System.NotImplementedException()
            };

            private Vector2 ValueToGUIPoint(Vector2 value) {
                return rect.position + ValueToGraphPoint(value);
            }

            private Vector2 ValueToGraphPoint(Vector2 value) {
                var tx = (value.x - xAxis.range.Min) / xAxis.range.Length;
                var ty = (value.y - yAxis.range.Min) / yAxis.range.Length;
                return new Vector2(
                    Mathf.LerpUnclamped(0, rect.width, tx),
                    Mathf.LerpUnclamped(rect.height, 0, ty));
            }


            /// ----------------------------------------------------------------------------
            private static class Style {
                public static GUIStyle label;
                static Style() {
                    label = new GUIStyle(GUI.skin.label) {
                        alignment = TextAnchor.MiddleCenter,
                    };
                    label.normal.textColor = Colors.White;
                }
            }
        }

    }


}

#endif