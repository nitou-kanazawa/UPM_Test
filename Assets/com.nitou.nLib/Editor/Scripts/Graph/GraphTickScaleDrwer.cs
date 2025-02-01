#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Nitou.EditorShared {
	public partial class GUIGraph {

		public IGraphAxisSettings xAxis { get; }
		public IGraphAxisSettings yAxis { get; }


		public interface IGraphAxisSettings {

			/// <summary>
			/// 軸の値域
			/// </summary>
			public RangeFloat Range { get; }

			/// <summary>
			/// 主目盛り幅
			/// </summary>
			public float MainStep { get; }

			/// <summary>
			/// 副目盛り幅
			/// </summary>
			public float SubStep { get; }

			/// <summary>
			/// 目盛り自動設定を行うかどうか
			/// </summary>
			//public bool AutoScaling { get; }
		}


		public class GraphTickScaleDrwer : GraphComponentDrawer {

			public GraphTickScaleDrwer(GUIGraph context) : base(context){ }

			public void Draw(Rect graphRect) {

			}

			/// <summary>
			/// 
			/// </summary>
			public void DrawVerticalLine(Rect rect, float x) {
				var p1 = GetGraphPoint(new Vector2(x, yAxis.Range.Min), rect);
				var p2 = GetGraphPoint(new Vector2(x, yAxis.Range.Max), rect);
				//handles
			}
		}

	}
}
#endif