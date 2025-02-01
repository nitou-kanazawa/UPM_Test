using UnityEngine;
using TMPro;
using Nitou;

namespace Project
{

    [ExecuteAlways]
    public class Test_RectViewport : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _minText;
        [SerializeField] TextMeshProUGUI _maxText;

        private RectTransform _rectTrans;
        private RectTransform _canvasRectTrans;

        private Canvas _canvas;


        void Update()
        {
            if (_rectTrans is null || _canvasRectTrans is null) {
                _rectTrans = GetComponent<RectTransform>();
                _canvasRectTrans = _rectTrans.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            }

            //var viewport =  RectUtil.GetRelativeRect(_rectTrans.rect, _canvasRectTrans.rect);
            var viewport = _rectTrans.GetViewportRect(ref _canvas);

            if (_minText != null && _maxText != null) {
                _minText.text = viewport.min.ToString();
                _maxText.text = viewport.max.ToString();
            }

        }
    }
}
