using UnityEngine;

namespace Nitou {

    /// <summary>
    /// <see cref="Rigidbody"/>の基本的な拡張メソッド集．
    /// </summary>
    public static partial class RigidbodyExtensions {

        /// ----------------------------------------------------------------------------
        #region Basic

        /// <summary>
        /// ���[�J�����W���擾����g�����\�b�h
        /// </summary>
        public static Vector3 GetLocalPosition(this Rigidbody self) {
            return self.transform.parent.InverseTransformPoint(self.position);
        }

        /// <summary>
        /// ���[�J����]���擾����g�����\�b�h
        /// </summary>
        public static Quaternion GetLocalRotation(this Rigidbody self) {
            return Quaternion.Inverse(self.transform.parent.rotation) * self.rotation;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Move

        /// <summary>
        /// Local���W���w�肵���ړ����\�b�h
        /// </summary>
        public static void MoveLocalPosition(this Rigidbody self, Vector3 localPosition) {
            var position = self.transform.parent.TransformPoint(localPosition);
            self.MovePosition(position);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void MovePositionRelative(this Rigidbody self, Vector3 deltaPosition) {
            self.MovePosition(self.position + deltaPosition);
        }

        /// <summary>
        /// Local���W���w�肵����]���\�b�h
        /// </summary>
        public static void MoveLocalRotaion(this Rigidbody self, Quaternion localRotation) {
            var rotation = self.transform.parent.rotation * localRotation;
            self.MoveRotation(rotation);
        }
        #endregion


        /// <summary>
        /// ���x�����Z�b�g����
        /// </summary>
        public static void ResetVelocity(this Rigidbody self) {

#if UNITY_6000_0_OR_NEWER
            self.linearVelocity = Vector3.zero;
#else
            self.velocity = Vector3.zero;
#endif
            self.angularVelocity = Vector3.zero;
        }
    }
}
