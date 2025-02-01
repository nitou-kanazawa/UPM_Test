using UnityEngine;

namespace Nitou{

    /// <summary>
    /// <see cref="Collider"/>型の基本的な拡張メソッド集．
    /// </summary>
    public static class ColliderExtensions{

        /// ----------------------------------------------------------------------------
        #region Setter (メソッドチェーン用)

        /// <summary>
        /// <see cref="Collider.isTrigger"/>を設定する拡張メソッド．
        /// </summary>
        public static TCollider SetTrigger<TCollider>(this TCollider self, bool isTrigger) 
            where TCollider : Collider{
            
            self.isTrigger = isTrigger;
            return self;
        }



        /// <summary>
        /// <see cref="Collider.material"/>を設定する拡張メソッド．
        /// </summary>
#if UNITY_6000_0_OR_NEWER
        public static TCollider SetMaterial<TCollider>(this TCollider self, PhysicsMaterial material)
            where TCollider : Collider {
            
            self.material = material;
            return self;
        }
#else
       public static TCollider SetMaterial<TCollider>(this TCollider self, PhysicMaterial material)
            where TCollider : Collider {
            
            self.material = material;
            return self;
        }

#endif

        #endregion


        /// ----------------------------------------------------------------------------
        #region Center Position

        /// <summary>
        /// グローバル座標に変換したコライダー中心座標を取得する拡張メソッド．
        /// </summary>
        public static Vector3 GetWorldCenter(this Collider self) {        
            if(self is BoxCollider box)  return box.GetWorldCenter();
            else if(self is SphereCollider sphere) return sphere.GetWorldCenter();
            else if(self is CapsuleCollider capcel) return capcel.GetWorldCenter();

            return self.transform.position;
        }        
#endregion
    }
}
