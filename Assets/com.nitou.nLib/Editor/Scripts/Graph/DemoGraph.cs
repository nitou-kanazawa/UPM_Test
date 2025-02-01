#if UNITY_EDITOR
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Nitou.EditorScripts {

	// デモ表示用のエディタウインドウ
	public class DemoEditorWindow : EditorWindow {

		/// <summary>
		/// 
		/// </summary>
		[MenuItem(ToolBarMenu.Prefix.Develop + "Test/Demo Graph")]
		public static void Open() => GetWindow<DemoEditorWindow>("Demo Graph");

		private void OnGUI() {
			EditorGUILayout.Space();
			GUILayout.Label("-------------");
			{ // グラフ描画
				var settings = new AxisSettings(xRange: (0, 10), yRange: (0, 10), xStep: 1, yStep: 2);
				var dummyData = EnumerableUtils.LinspaceWithStep(-2, 12, 0.5f)
					.Select(x => new Vector2(x, Mathf.Sin(x) + x))
					.ToArray();

				var rect = GUILayoutUtility.GetRect(300, 200).Padding(10);
				DrawBackDrop(rect, Colors.backdropColor, Colors.gridLineColor);
				DrawGrid(rect, settings, Colors.gridLineColor);
				DrawData(rect, settings, dummyData, Colors.dataColor);
			}
			GUILayout.Label("-------------");
		}

		/// <summary>
		/// 点列を描画する
		/// </summary>
		public void DrawData(Rect rect, AxisSettings settings, Vector2[] values, Color color) {
			using (new GUI.ClipScope(rect)) // ※スコープ内のGUIメソッドは指定Rectを基準として処理される
			using (new Handles.DrawingScope(color)) {
				for (int i = 0; i < values.Length - 1; i++) {
					Vector2 p1 = Convertor.ValueToGraphPoint(values[i], rect.size, settings);
					Vector2 p2 = Convertor.ValueToGraphPoint(values[i + 1], rect.size, settings);
					Handles.DrawLine(p1, p2);
				}

				foreach (var value in values) {
					Vector2 p = Convertor.ValueToGraphPoint(value, rect.size, settings);
					Handles.DrawSolidDisc(p, Vector3.forward, 2f);
				}
			}
		}


		public void DrawGrid(Rect rect, AxisSettings settings, Color gridColor) {
			using (new Handles.DrawingScope(gridColor)) {
				var graphOrigin = rect.position;

				// X軸
				var xStart = Mathf.Ceil(settings.xRange.min / settings.xStep) * settings.xStep;
				var xTicks = EnumerableUtils.LinspaceWithStep(xStart, settings.xRange.max, settings.xStep);
				foreach (var x in xTicks) {
					var value1 = new Vector2(x, settings.yRange.min);
					var value2 = new Vector2(x, settings.yRange.max);

					// GUI座標に変換して描画
					var p1 = Convertor.ValueToGraphPoint(value1, rect.size, settings) + graphOrigin;
					var p2 = Convertor.ValueToGraphPoint(value2, rect.size, settings) + graphOrigin;
					Handles.DrawLine(p1, p2);
				}

				// Y軸
				var yStart = Mathf.Ceil(settings.yRange.min / settings.yStep) * settings.yStep;
				var yTicks = EnumerableUtils.LinspaceWithStep(yStart, settings.yRange.max, settings.yStep);
				foreach (var y in yTicks) {
					var value1 = new Vector2(settings.xRange.min, y);
					var value2 = new Vector2(settings.xRange.max, y);

					// GUI座標に変換して描画
					var p1 = Convertor.ValueToGraphPoint(value1, rect.size, settings) + graphOrigin;
					var p2 = Convertor.ValueToGraphPoint(value2, rect.size, settings) + graphOrigin;
					Handles.DrawLine(p1, p2);
				}
			}
		}


		/// <summary>
		/// 背景と枠線を描画する
		/// </summary>
		public void DrawBackDrop(Rect rect, Color backdropColor, Color outlineColor) {
			Handles.DrawSolidRectangleWithOutline(rect, backdropColor, outlineColor);
		}

		private static class Colors {
			public static readonly Color backdropColor = new Color(0.8f, 0.8f, 0.8f, 0.1f);
			public static readonly Color gridLineColor = new Color(0.9f, 0.9f, 0.9f, 0.5f);
			public static readonly Color dataColor = Color.green;
		}


		public struct AxisSettings {
			// グラフの値域
			public (float min, float max) xRange;
			public (float min, float max) yRange;

			// グリッド幅
			public float xStep;
			public float yStep;

			public AxisSettings((float min, float max) xRange, (float min, float max) yRange, float xStep, float yStep) {
				this.xRange = xRange;
				this.yRange = yRange;
				this.xStep = xStep;
				this.yStep = yStep;
			}
		}

		private static class Convertor {

			// グラフの値域
			public static (float min, float max) xRange = (0, 10);
			public static (float min, float max) yRange = (0, 10);

			/// <summary>
			/// グラフ範囲の座標に変換する
			/// </summary>
			public static Vector2 ValueToGraphPoint(Vector2 value, Vector2 size, AxisSettings settings) {
				var xRange = settings.xRange;
				var yRange = settings.yRange;
				float x = Mathf.Lerp(0, size.x, (value.x - xRange.min) / (xRange.max - xRange.min));
				float y = Mathf.Lerp(size.y, 0, (value.y - yRange.min) / (yRange.max - yRange.min));
				return new Vector2(x, y);
			}

		}
	}
}
#endif