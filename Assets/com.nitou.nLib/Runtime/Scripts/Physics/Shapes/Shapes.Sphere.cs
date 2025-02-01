using UnityEngine;

namespace Nitou {
    public static partial class Shapes {

        /// <summary>
        /// 球体の形状を表すインスタンス
        /// </summary>
        [System.Serializable]
        public class Sphere : Volume{

            /// <summary>
            /// 半径
            /// </summary>
            public float radius = 1f;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Sphere(Vector3 position, Quaternion rotation, float radius)
                : base(position, rotation) {
                this.radius = radius;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Sphere(Transform transform, float radius)
                : base(transform) {
                this.radius = radius;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            private Sphere() { }


            public override string ToString() {
                return $"[Sphere] position: {position}, rotation: {eulerAngle}, radius: {radius}";
            }
        }
    }
}
