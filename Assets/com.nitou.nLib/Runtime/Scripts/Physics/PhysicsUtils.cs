using UnityEngine;

namespace Nitou{

    public static partial class PhysicsUtils{

        /// <summary>
        /// <see cref="Shapes.Box"/>と交差している<see cref="Collider"/>を全て取得してキャッシュする
        /// </summary>
        public static int OverlapBoxNonAlloc(Shapes.Box box, Collider[] results,int layerMask) {

            return Physics.OverlapBoxNonAlloc(
                box.position,
                box.size.Half(),
                results,
                box.rotation,
                layerMask);
        }

        /// <summary>
        /// <see cref="Shapes.Box"/>と交差している<see cref="Collider"/>を全て取得してキャッシュする
        /// </summary>
        public static int OverlapBoxNonAlloc(Transform trans, Shapes.Box box, Collider[] results, int layerMask) {

            var position = box.GetWorldPosition(trans);
            var rotation = box.GetWorldRotaion(trans);

            return Physics.OverlapBoxNonAlloc(
                position,
                box.size.Half(),
                results,
                rotation,
                layerMask);
        }



    }
}
