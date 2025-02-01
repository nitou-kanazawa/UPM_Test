#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Nitou.EditorShared {

    /// <summary>
    /// GUIにグラフを表示するクラス
    /// </summary>
    public partial class GUIGraph {


		public class GraphSettings {

        }

		/// <summary>
		/// グラフ要素の描画を担当するクラス
		/// </summary>
		public abstract class GraphComponentDrawer {
			public readonly GUIGraph context;

			public IGraphAxisSettings xAxis => context.xAxis;
			public IGraphAxisSettings yAxis => context.yAxis;

			public GraphComponentDrawer(GUIGraph context) {
				this.context = context ?? throw new System.ArgumentNullException(nameof(context));
			}



			protected Vector2 GetGraphPoint(Vector2 value, Rect rect) {
				return Convertor.ValueToGraphPoint(value, rect, xAxis, yAxis);
			}


			/// <summary>
			/// 座標変換用のクラス (※描画クラス以外がアクセスする必要はない)
			/// </summary>
			protected static class Convertor {

				/// <summary>
				/// 
				/// </summary>
				public static Vector2 ValueToGraphPoint(Vector2 value, Rect rect, 
					IGraphAxisSettings xAxis, IGraphAxisSettings yAxis) {

					var tx = (value.x - xAxis.Range.Min) / xAxis.Range.Length;
					var ty = (value.y - yAxis.Range.Min) / yAxis.Range.Length;
					
					return new Vector2(
						Mathf.LerpUnclamped(0, rect.width, tx),
						Mathf.LerpUnclamped(rect.height, 0, ty));
				}

			}
        }
    }


    public class TestGraphWindow : EditorWindow {

        private GUIGraph.GraphPlotArea _plotArea;
        private GUIGraph.GraphAxis _xAxis;
        private GUIGraph.GraphAxis _yAxis;

        private Vector2[] _points;

        /// <summary>
        /// 
        /// </summary>
        [MenuItem(ToolBarMenu.Prefix.Develop + "Test Graph")]
        public static void Open() {
            GetWindow<TestGraphWindow>("Test Graph");
        }

        private void OnGUI() {
            _points = CreateDammyData();


            GUILayout.Label("Test Graph", EditorStyles.boldLabel);
            EditorUtil.GUI.HorizontalLine();

            var rect = GUILayoutUtility.GetRect(300, 200).Padding(60,20,20,30);
            {
                _xAxis = new GUIGraph.GraphAxis();
                _yAxis = new GUIGraph.GraphAxis();
                _plotArea = new GUIGraph.GraphPlotArea(rect, _xAxis, _yAxis);

                _plotArea.DrawBackDrop(GraphColors.backdropColor, GraphColors.outlineColor);
                
				// Grid
				_plotArea.DrawGridX(GraphColors.gridColor);
				_plotArea.DrawGridY(GraphColors.gridColor);

				// Axis Label
				_plotArea.DrawAxisTitleX();
				_plotArea.DrawAxisTitleY();

                _plotArea.DrawData(_points, Colors.GreenYellow);
            }

            EditorUtil.GUI.HorizontalLine();
        }

        private static Vector2[] CreateDammyData() {
            return new Vector2[]{
            new Vector2(-2, 3),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(3, 4),
            new Vector2(4, 2),
            new Vector2(5, 5),
            new Vector2(6, 3),
            new Vector2(7, 2),
            new Vector2(9, 9),
            new Vector2(10, 10),
            new Vector2(12, 2),
        };
        }


        public static class GraphColors {
            public static Color backdropColor = new Color(.6f, .6f, .6f, .1f);
            public static Color outlineColor = new Color(.8f, .8f, .8f, .3f);
            public static Color gridColor = new Color(.8f, .8f, .8f, .3f);
        }
    }
}
#endif