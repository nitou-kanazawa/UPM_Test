using UnityEngine;

// [REF]
//  _: Gizmosで平面を描画する https://nyama41.hatenablog.com/entry/draw_gizmos_plane

namespace Nitou {
    public partial class GizmoUtil {

        /// <summary>
        /// Gizmoに<see cref="Color"/>を適用するスコープ
        /// </summary>
        public struct ColorScope : System.IDisposable {

            private readonly Color _oldColor;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public ColorScope(Color color) {
                _oldColor = Gizmos.color;
                Gizmos.color = color;
            }

            /// <summary>
            /// 終了処理
            /// </summary>
            public void Dispose() {
                Gizmos.color = _oldColor;
            }
        }


        /// <summary>
        /// Gizmoに<see cref="Matrix4x4"/>を適用するスコープ
        /// </summary>
        public struct MatrixScope : System.IDisposable {

            private readonly Matrix4x4 _oldMatrix;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public MatrixScope(Matrix4x4 matrix) {
                _oldMatrix = Gizmos.matrix;
                Gizmos.matrix = matrix;
            }

            /// <summary>
            /// 終了処理
            /// </summary>
            public void Dispose() {
                Gizmos.matrix = _oldMatrix;
            }
        }
    }
}
