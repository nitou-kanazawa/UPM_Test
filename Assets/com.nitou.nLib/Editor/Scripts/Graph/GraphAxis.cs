using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Nitou.EditorShared {
    public partial class GUIGraph {

        public interface IGraphAxis {
            RangeFloat Range { get; }
            float Step { get; }
        }

		/// <summary>
		/// グラフ軸に付随するデータ
		/// </summary>
        public class GraphAxis : IGraphAxis {

            // 値域
            public RangeFloat range = new(0, 10);
            public float step = 1f;

            // 
            public string label = "Axis title [-]";

            // 表示フラグ
            public bool showTicks = true;
            public bool showTitleLabel;


            /// ----------------------------------------------------------------------------
            // Property

            public RangeFloat Range => range;

            public float Step => step;


            /// ----------------------------------------------------------------------------
            // Public Method

            public override string ToString() {
                return $"GraphAxis:\n" +
                       $"  Range: {range}\n" +
                       $"  Step: {step}\n" +
                       $"  Label: {label}\n" +
                       $"  Show Ticks: {showTicks}\n" +
                       $"  Show Title Label: {showTitleLabel}";
            }
        }

        public static class Test {

            [MenuItem("Develop/Foo")]
            static void Foo() {
                var axis = new GraphAxis();
                Debug.Log(axis);
            }
        }

    }
}