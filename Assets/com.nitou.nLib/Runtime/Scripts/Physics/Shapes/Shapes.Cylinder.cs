using UnityEngine;

namespace Nitou {
    public partial class Shapes {

        /// <summary>
        /// 円柱の形状を表すインスタンス．
        /// </summary>
        [System.Serializable]
        public class Cylinder : Volume {

            /// <summary>
            /// 半径．
            /// </summary>
            public float radius = 0.5f;

            /// <summary>
            /// 高さ．
            /// </summary>
            public float height = 1.5f;


            /// ----------------------------------------------------------------------------
            // Public Method

            public Cylinder(Vector3 position, Quaternion rotation, float radius, float height)
                : base(position, rotation) {
                this.radius = radius;
                this.height = height;
            }

            private Cylinder() { }

            public override string ToString() {
                return $"[Box] position: {position}, rotation: {eulerAngle}, radius: {radius}, height: {height}";
            }
        }
    }
}
