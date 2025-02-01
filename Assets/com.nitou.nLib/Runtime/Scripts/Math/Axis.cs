using UnityEngine;

namespace Nitou {

    /// <summary>
    /// 直行軸を指定するための列挙型．
    /// </summary>
    public enum Axis {
        X,
        Y,
        Z,
    }

    public static class AxsisExtensions {

        public static Vector3 ToVector3(this Axis axis) {
            return axis switch {
                Axis.X => Vector3.right,
                Axis.Y => Vector3.up,
                Axis.Z => Vector3.forward,
                _ => throw new System.NotImplementedException()
            };
        }
    }

}
