using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// [REF]
//  コガネブログ: uGUI で円形のレイアウトを使用できる「UnityRadialLayoutGroup」紹介 https://baba-s.hatenablog.com/entry/2020/02/26/090000
//  github: aillieo/UnityRadialLayoutGroup https://github.com/aillieo/UnityRadialLayoutGroup/tree/master

namespace Nitou.UI.Component {

    [AddComponentMenu(ComponentMenu.Prefix.Layout + "Radial Layout Group")]
    [DisallowMultipleComponent]
    public class RadialLayoutGroup : LayoutGroup {

        /// ----------------------------------------------------------------------------
        #region Inner Definiton

        public enum Direction {
            Clockwise = 0,
            Counterclockwise = 1,
            Bidirectional = 2
        }

        public enum ConstraintMode {
            Interval = 0,
            Range = 1
        }
        #endregion

        private static readonly Vector2 center = new Vector2(0.5f, 0.5f);


        /// ----------------------------------------------------------------------------

        private List<RectTransform> _childList = new ();
        private List<ILayoutIgnorer> _ignoreList = new ();


        [SerializeField] private ConstraintMode _angleConstraint;
        public ConstraintMode AngleConstraint {
            get => _angleConstraint;
            set => SetProperty(ref _angleConstraint, value);
        }

        [SerializeField] private ConstraintMode _radiusConstraint;
        public ConstraintMode RadiusConstraint {
            get => _radiusConstraint;
            set => SetProperty(ref _radiusConstraint, value);
        }

        [SerializeField] private Direction _layoutDir;
        public Direction LayoutDir {
            get => _layoutDir;
            set => SetProperty(ref _layoutDir, value);
        }

        [SerializeField] private float _radiusStart;
        public float RadiusStart {
            get => _radiusStart;
            set => SetProperty(ref _radiusStart, value);
        }

        [SerializeField] private float _radiusDelta;
        public float RadiusDelta {
            get => _radiusDelta;
            set => SetProperty(ref _radiusDelta, value);
        }

        [SerializeField] private float _radiusRange;
        public float RadiusRange {
            get => _radiusRange;
            set => SetProperty(ref _radiusRange, value);
        }

        [SerializeField] private float _angleDelta;
        public float AngleDelta {
            get => _angleDelta;
            set => SetProperty(ref _angleDelta, value);
        }

        [SerializeField] private float _angleStart;
        public float AngleStart {
            get => _angleStart;
            set => SetProperty(ref _angleStart, value);
        }

        [SerializeField] private float _angleCenter;
        public float AngleCenter {
            get => _angleCenter;
            set => SetProperty(ref _angleCenter, value);
        }

        [SerializeField] private float _angleRange;
        public float AngleRange {
            get => _angleRange;
            set => SetProperty(ref _angleRange, value);
        }

        [SerializeField] private bool _childRotate = false;
        public bool ChildRotate {
            get => _childRotate;
            set => SetProperty(ref _childRotate, value);
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        public override void CalculateLayoutInputVertical() {
        }

        public override void CalculateLayoutInputHorizontal() {
        }

        public override void SetLayoutHorizontal() {
            CalculateChildrenPositions();
        }

        public override void SetLayoutVertical() {
            CalculateChildrenPositions();
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void CalculateChildrenPositions() {
            this.m_Tracker.Clear();

            _childList.Clear();

            for (int i = 0; i < this.transform.childCount; ++i) {
                RectTransform rect = this.transform.GetChild(i) as RectTransform;

                if (!rect.gameObject.activeSelf) {
                    continue;
                }

                _ignoreList.Clear();
                rect.GetComponents(_ignoreList);
                if (_ignoreList.Count == 0) {
                    _childList.Add(rect);
                    continue;
                }

                for (int j = 0; j < _ignoreList.Count; j++) {
                    if (!_ignoreList[j].ignoreLayout) {
                        _childList.Add(rect);
                        break;
                    }
                }

                _ignoreList.Clear();
            }

            EnsureParameters(_childList.Count);

            for (int i = 0; i < _childList.Count; ++i) {
                var child = _childList[i];
                float delta = i * _angleDelta;
                float angle = LayoutDir == Direction.Clockwise ? _angleStart - delta : _angleStart + delta;
                ProcessOneChild(child, angle, _radiusStart + (i * _radiusDelta));
            }

            _childList.Clear();
        }

        private void EnsureParameters(int childCount) {
            EnsureAngleParameters(childCount);
            EnsureRadiusParameters(childCount);
        }

        private void EnsureAngleParameters(int childCount) {
            int intervalCount = childCount - 1;
            switch (LayoutDir) {
                case Direction.Clockwise:
                    if (AngleConstraint == ConstraintMode.Interval) {
                        // mAngleDelta mAngleStart
                        this._angleRange = intervalCount * this._angleDelta;
                    } else {
                        // mAngleRange mAngleStart
                        if (intervalCount > 0) {
                            this._angleDelta = this._angleRange / intervalCount;
                        } else {
                            this._angleDelta = 0;
                        }
                    }
                    break;
                case Direction.Counterclockwise:
                    if (AngleConstraint == ConstraintMode.Interval) {
                        // mAngleDelta mAngleStart
                        this._angleRange = intervalCount * this._angleDelta;
                    } else {
                        // mAngleRange mAngleStart
                        if (intervalCount > 0) {
                            this._angleDelta = this._angleRange / intervalCount;
                        } else {
                            this._angleDelta = 0;
                        }
                    }
                    break;
                case Direction.Bidirectional:
                    if (AngleConstraint == ConstraintMode.Interval) {
                        // mAngleDelta mAngleCenter
                        this._angleRange = intervalCount * this._angleDelta;
                    } else {
                        // mAngleRange mAngleCenter
                        if (intervalCount > 0) {
                            this._angleDelta = this._angleRange / intervalCount;

                        } else {
                            this._angleDelta = 0;
                        }
                    }
                    this._angleStart = this._angleCenter - _angleRange * 0.5f;
                    break;
            }
        }

        private void EnsureRadiusParameters(int childCount) {
            int intervalCount = childCount - 1;
            switch (LayoutDir) {
                case Direction.Clockwise:
                    if (RadiusConstraint == ConstraintMode.Interval) {
                        // mRadiusDelta mRadiusStart
                        this._radiusRange = intervalCount * this._radiusDelta;
                    } else {
                        // mRadiusRange mRadiusStart
                        if (intervalCount > 0) {
                            this._radiusDelta = _radiusRange / intervalCount;
                        } else {
                            this._radiusDelta = 0;
                        }
                    }
                    break;
                case Direction.Counterclockwise:
                case Direction.Bidirectional:
                    if (RadiusConstraint == ConstraintMode.Interval) {
                        // mRadiusDelta mRadiusStart
                        this._radiusRange = intervalCount * this._radiusDelta;
                    } else {
                        // mRadiusRange mRadiusStart
                        if (intervalCount > 0) {
                            this._radiusDelta = _radiusRange / intervalCount;
                        } else {
                            this._radiusDelta = 0;
                        }
                    }
                    break;
            }
        }


        private void ProcessOneChild(RectTransform child, float angle, float radius) {
            Vector3 pos = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0.0f);
            child.localPosition = pos * radius;

            DrivenTransformProperties drivenProperties =
                DrivenTransformProperties.Anchors | DrivenTransformProperties.AnchoredPosition | DrivenTransformProperties.Rotation | DrivenTransformProperties.Pivot;
            m_Tracker.Add(this, child, drivenProperties);

            child.anchorMin = center;
            child.anchorMax = center;
            child.pivot = center;

            if (this.ChildRotate) {
                child.localEulerAngles = new Vector3(0, 0, angle);
            } else {
                child.localEulerAngles = Vector3.zero;
            }

        }

        //#if UNITY_EDITOR
        //        protected override void OnValidate()
        //        {
        //            base.OnValidate();
        //            CalculateChildrenPositions();
        //        }
        //#endif
    }
}
