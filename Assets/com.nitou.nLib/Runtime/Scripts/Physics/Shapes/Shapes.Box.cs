using System;
using UnityEngine;

namespace Nitou {
    public partial class Shapes {

        /// <summary>
        /// 直方体の形状を表すインスタンス．
        /// </summary>
        [Serializable]
        public class Box : Volume {

            /// <summary>
            /// サイズ．
            /// </summary>
            public Vector3 size = Vector3.zero;


            /// ----------------------------------------------------------------------------
            // Public Method

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(Vector3 position, Quaternion rotation, Vector3 size)
                : base(position, rotation) {
                this.size = size;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(Transform transform, Vector3 size)
                : base(transform) {
                this.size = size;
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public Box(BoxCollider collider) {
                this.position = collider.GetWorldCenter();
                this.eulerAngle = collider.transform.rotation.eulerAngles;
                this.size = collider.GetScaledSize();
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            private Box(){}

            public override string ToString() {
                return $"[Box] position: {position}, rotation: {eulerAngle}, size: {size}";
            }
        }

    }

}
