using UnityEngine;

namespace Nitou {

    /// <summary>
    /// Transformの"position"と"rotation"のみを扱うデータ構造体．
    /// </summary>
    [System.Serializable]
    public struct Coord {

        public Vector3 position;
        public Quaternion rotation;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Coord(Vector3 position, Quaternion rotation) {
            this.position = position;
            this.rotation = rotation;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Coord(Transform transform) {
            this.position = transform.position;
            this.rotation = transform.rotation;
        }
    }


    public static partial class TransformExtension {

        /// <summary>
        /// 位置と姿勢を一括設定する拡張メソッド
        /// </summary>
        public static void SetPositionAndRotation(this Transform self, Coord coord) =>
            self.SetPositionAndRotation(coord.position, coord.rotation);
    }
}
