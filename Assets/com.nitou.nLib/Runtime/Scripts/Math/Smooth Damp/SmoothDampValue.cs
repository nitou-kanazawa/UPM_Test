using UnityEngine;

// [REF]
//  ねこじゃらしティ: SmoothDampで滑らかな追従を実装する https://nekojara.city/unity-smooth-damp
//  LIGHT11: Lerpを用いたスムージングの問題点とMathf.SmoothDampによる解決策 https://light11.hatenadiary.com/entry/2021/06/01/203624
//  _ : SmoothDampを構造体化して使いやすくする https://tech.ftvoid.com/smooth-damp-struct

namespace Nitou {

    /// ----------------------------------------------------------------------------
    #region Float

    /// <summary>
    /// 値を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDamp）
    /// </summary>
    public class SmoothDampFloat : ISmoothDampValue<float>{

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SmoothDampFloat(float initialValue) {
            _current = initialValue;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Mathf.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// 変数をリセットする
        /// </summary>
        public void Reset(float value, float velocity) {
            _current = value;
            _currentVelocity = velocity;
        }
    }

    /// <summary>
    /// 角度を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDampAngle）
    /// </summary>
    public class SmoothDampAngle : ISmoothDampValue<float>{

        private float _current;
        private float _currentVelocity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SmoothDampAngle(float initialValue) {
            _current = initialValue;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }
        
        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public float GetNext(float target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Mathf.SmoothDampAngle(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }
    }
    #endregion


    /// ----------------------------------------------------------------------------
    #region Vector2

    /// <summary>
    /// 値を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDamp）
    /// </summary>
    public class SmoothDampVector2 : ISmoothDampValue<Vector2>{

        private Vector2 _current;
        private Vector2 _currentVelocity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SmoothDampVector2(Vector2 initialValue) {
            _current = initialValue;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime, float maxSpeed) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector2 GetNext(Vector2 target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Vector2.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// 変数をリセットする
        /// </summary>
        public void Reset(Vector2 value, Vector2 velocity) {
            _current = value;
            _currentVelocity = velocity;
        }
    }
    #endregion


    /// ----------------------------------------------------------------------------
    #region Vector3

    /// <summary>
    /// 値を目標値に滑らかに追従させる構造体
    /// （※Mathf.SmoothDamp）
    /// </summary>
    public class SmoothDampVector3 : ISmoothDampValue<Vector3> {

        private Vector3 _current;
        private Vector3 _currentVelocity;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SmoothDampVector3(Vector3 initialValue) {
            _current = initialValue;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector3 GetNext(Vector3 target, float smoothTime, float maxSpeed, float deltaTime) {
            _current = Vector3.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed,
                deltaTime
            );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector3 GetNext(Vector3 target, float smoothTime, float maxSpeed) {
            _current = Vector3.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime,
                maxSpeed
            );

            return _current;
        }

        /// <summary>
        /// 平滑化された値を取得する
        /// </summary>
        public Vector3 GetNext(Vector3 target, float smoothTime) {
            _current = Vector3.SmoothDamp(
                _current,
                target,
                ref _currentVelocity,
                smoothTime
                );

            return _current;
        }
    }
    #endregion

}
