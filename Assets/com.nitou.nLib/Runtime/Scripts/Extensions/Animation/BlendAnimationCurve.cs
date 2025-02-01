using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// [参考]
//  kanのメモ帳: 複数のAnimationCurveの波形をブレンド(合成)して使う BlendAnimationCurve https://kan-kikuchi.hatenablog.com/entry/BlendAnimationCurve

namespace Nitou {

    [System.Serializable]
    public class BlendAnimationCurve{

        //カーブとその重みのペア
        [System.Serializable]
        public struct CurveWeightPair {
            
            [SerializeField] AnimationCurve _curve;
            [SerializeField] float _weight;

            public AnimationCurve Curve => _curve;
            public float Weight => _weight;

            public CurveWeightPair(AnimationCurve curve, float weight) {
                _curve = curve;
                _weight = weight;
            }
        }


        [SerializeField] List<CurveWeightPair> _curveWeightPairs = new ();


        /// ----------------------------------------------------------------------------
        // Public Method

        public BlendAnimationCurve() { }

        public BlendAnimationCurve(params AnimationCurve[] curves) {
            _curveWeightPairs = curves.Select(curve => new CurveWeightPair(curve, 1)).ToList();
        }

        public BlendAnimationCurve(params CurveWeightPair[] curveWeightPairs) {
            _curveWeightPairs = curveWeightPairs.ToList();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// 重みを指定してカーブを追加
        /// </summary>
        public void Add(AnimationCurve curve, float weight = 1.0f) {
            _curveWeightPairs.Add(new CurveWeightPair(curve, weight));
        }

        /// <summary>
        /// 指定時間の値を取得
        /// </summary>
        public float Evaluate(float time) {
            if (_curveWeightPairs.Count == 0) {
                Debug.LogError($"CurveWeightPairが設定されていません");
                return 0;
            }

            var totalWeight = 0f;
            var blendedValue = 0f;

            foreach (var curveWeightPair in _curveWeightPairs) {
                totalWeight += curveWeightPair.Weight;
                blendedValue += curveWeightPair.Curve.Evaluate(time) * curveWeightPair.Weight;
            }

            if (totalWeight > 0f) {
                blendedValue /= totalWeight;
            } else {
                Debug.LogWarning($"Weightの合計が0以下です : {totalWeight}");
            }

            return blendedValue;
        }

        /// <summary>
        /// DotweenのEase用の取得メソッド
        /// </summary>
        public float EaseEvaluate(float time, float duration, float overshootOrAmplitude, float period) {
            if (duration <= 0) {
                return 0f;
            }
            return Evaluate(time / duration);
        }
    }
}
