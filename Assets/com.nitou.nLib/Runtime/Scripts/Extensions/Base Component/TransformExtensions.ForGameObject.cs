using UnityEngine;

namespace Nitou {

    /// <summary>
    /// GameObject型の拡張メソッドを管理するクラス
    /// </summary>
    public static partial class GameObjectExtensions {

        /// ----------------------------------------------------------------------------
        #region 位置の設定

        /// <summary>
        /// 位置を設定します
        /// </summary>
        public static void SetPosition(this GameObject self, Vector3 position) {
            self.transform.position = position;
        }

        /// <summary>
        /// X座標を設定します
        /// </summary>
        public static void SetPositionX(this GameObject self, float x) {
            self.transform.SetPositionX(x);
        }

        /// <summary>
        /// Y座標を設定します
        /// </summary>
        public static void SetPositionY(this GameObject self, float y) {
            self.transform.SetPositionY(y);
        }

        /// <summary>
        /// Z座標を設定します
        /// </summary>
        public static void SetPositionZ(this GameObject self, float z) {
            self.transform.SetPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系の位置を設定します
        /// </summary>
        public static void SetLocalPosition(this GameObject self, Vector3 localPosition) {
            self.transform.localPosition = localPosition;
        }

        /// <summary>
        /// ローカル座標系のX座標を設定します
        /// </summary>
        public static void SetLocalPositionX(this GameObject self, float x) {
            self.transform.SetLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標を設定します
        /// </summary>
        public static void SetLocalPositionY(this GameObject self, float y) {
            self.transform.SetLocalPositionY(y);
        }

        /// <summary>
        /// ローカルのZ座標を設定します
        /// </summary>
        public static void SetLocalPositionZ(this GameObject self, float z) {
            self.transform.SetLocalPositionZ(z);
        }


        /// <summary>
        /// X座標に加算します
        /// </summary>
        public static void AddPositionX(this GameObject self, float x) {
            self.transform.AddPositionX(x);
        }

        /// <summary>
        /// Y座標に加算します
        /// </summary>
        public static void AddPositionY(this GameObject self, float y) {
            self.transform.AddPositionY(y);
        }

        /// <summary>
        /// Z座標に加算します
        /// </summary>
        public static void AddPositionZ(this GameObject self, float z) {
            self.transform.AddPositionZ(z);
        }

        /// <summary>
        /// ローカル座標系のX座標に加算します
        /// </summary>
        public static void AddLocalPositionX(this GameObject self, float x) {
            self.transform.AddLocalPositionX(x);
        }

        /// <summary>
        /// ローカル座標系のY座標に加算します
        /// </summary>
        public static void AddLocalPositionY(this GameObject self, float y) {
            self.transform.AddLocalPositionY(y);
        }

        /// <summary>
        /// ローカル座標系のZ座標に加算します
        /// </summary>
        public static void AddLocalPositionZ(this GameObject self, float z) {
            self.transform.AddLocalPositionZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 角度の設定

        /// <summary>
        /// 回転角を設定します
        /// </summary>
        public static void SetEulerAngle(this GameObject self, Vector3 eulerAngles) {
            self.transform.eulerAngles = eulerAngles;
        }

        /// <summary>
        /// X軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleX(this GameObject self, float x) {
            self.transform.SetEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleY(this GameObject self, float y) {
            self.transform.SetEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を設定します
        /// </summary>
        public static void SetEulerAngleZ(this GameObject self, float z) {
            self.transform.SetEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngle(this GameObject self, Vector3 localEulerAngles) {
            self.transform.localEulerAngles = localEulerAngles;
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleX(this GameObject self, float x) {
            self.transform.SetLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleY(this GameObject self, float y) {
            self.transform.SetLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のZ軸方向の回転角を設定します
        /// </summary>
        public static void SetLocalEulerAngleZ(this GameObject self, float z) {
            self.transform.SetLocalEulerAngleZ(z);
        }


        /// <summary>
        /// X軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleX(this GameObject self, float x) {
            self.transform.AddEulerAngleX(x);
        }

        /// <summary>
        /// Y軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleY(this GameObject self, float y) {
            self.transform.AddEulerAngleY(y);
        }

        /// <summary>
        /// Z軸方向の回転角を加算します
        /// </summary>
        public static void AddEulerAngleZ(this GameObject self, float z) {
            self.transform.AddEulerAngleZ(z);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleX(this GameObject self, float x) {
            self.transform.AddLocalEulerAngleX(x);
        }

        /// <summary>
        /// ローカル座標系のY軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleY(this GameObject self, float y) {
            self.transform.AddLocalEulerAngleY(y);
        }

        /// <summary>
        /// ローカル座標系のX軸方向の回転角を加算します
        /// </summary>
        public static void AddLocalEulerAngleZ(this GameObject self, float z) {
            self.transform.AddLocalEulerAngleZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region スケールの設定

        /// <summary>
        /// ローカル座標系の回転角を設定します
        /// </summary>
        public static void SetLocalScale(this GameObject self, Vector3 localScale) {
            self.transform.localScale = localScale;
        }

        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleX(this GameObject self, float x) {
            self.transform.SetLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleY(this GameObject self, float y) {
            self.transform.SetLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を設定します
        /// </summary>
        public static void SetLocalScaleZ(this GameObject self, float z) {
            self.transform.SetLocalScaleZ(z);
        }


        /// <summary>
        /// X軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleX(this GameObject self, float x) {
            self.transform.AddLocalScaleX(x);
        }

        /// <summary>
        /// Y軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleY(this GameObject self, float y) {
            self.transform.AddLocalScaleY(y);
        }

        /// <summary>
        /// Z軸方向のローカル座標系のスケーリング値を加算します
        /// </summary>
        public static void AddLocalScaleZ(this GameObject self, float z) {
            self.transform.AddLocalScaleZ(z);
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region 親子関係

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static GameObject SetParent(this GameObject self, Transform parent, bool worldPositionStays = true) {
            self.transform.SetParent(parent, worldPositionStays);
            return self;
        }

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static GameObject SetParent(this GameObject self, GameObject parent, bool worldPositionStays = true) {
            self.transform.SetParent(parent.transform, worldPositionStays);
            return self;
        }

        /// <summary>
        /// 親オブジェクトを設定する拡張メソッド
        /// </summary>
        public static void SetParentAndAlign(this GameObject self, GameObject parent) {
            if (parent == null) return;

            self.transform.SetParent(parent.transform, false);

            // 親と同じレイヤーと位置を与える (※GameObjectUtility.SetParentAndAlignと同じ)
            self.SetLayerRecursively(parent.layer);

            // 
            var rectTransform = self.transform as RectTransform;
            if (rectTransform) {
                rectTransform.anchoredPosition = Vector2.zero;
                Vector3 localPosition = rectTransform.localPosition;
                localPosition.z = 0;
                rectTransform.localPosition = localPosition;
            }
            // 
            else {
                self.transform.localPosition = Vector3.zero;
            }

            self.transform.localRotation = Quaternion.identity;
            self.transform.localScale = Vector3.one;
        }
        #endregion
    }
}
