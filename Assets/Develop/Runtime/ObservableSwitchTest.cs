using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Nitou;
using Nitou.UI;
using UniRx.Diagnostics;

// [REF]
//  Hatena: オペレータのSwitchについて https://shitakami.hateblo.jp/entry/2021/08/22/204549
//  qiita: UniTaskをCancellationTokenを指定しながらToObservableするメモ https://qiita.com/toRisouP/items/8ec18d73d9e8c5169587

namespace Project {

    public class ObservableSwitchTest : MonoBehaviour {

        [SerializeField] Image _image = null;
        [SerializeField] Sprite _defaultSprite; // デフォルト画像


        [Space]
        [SerializeField] TextMeshProUGUI _indexText;
        [SerializeField] Button _previousButton;
        [SerializeField] Button _nextButton;


        private readonly static string[] ResourceKeys = new string[]{
            "icon_a",
            "icon_b",
            "icon_c",
            "icon_d",
        };

        private ReactiveProperty<int> _currentIndex = new (0);


        private void Start() {

            _currentIndex.SubscribeToTextMeshPro(_indexText, index => $"{index}").AddTo(this);

            // 前後ボタンの設定
            _previousButton.onClick.AddListener(() => MoveIndex(-1));
            _nextButton.onClick.AddListener(() => MoveIndex(1));

            _currentIndex.
                DistinctUntilChanged()
                .Do(_ => ResetImageSprite())
                .Throttle(TimeSpan.FromSeconds(1f))
                .Select(index => ResourceKeys[index])
                .Select(path => ObservableConverter.FromUniTask(ct => LoadSpriteAsync(path, ct)))
                // 最新のIObservableに切り替える
                .Switch()
                .Subscribe(sprite => _image.sprite = sprite)
                .AddTo(this);
        }

        private void OnDestroy() {
            if(_image != null) {
                ResetImageSprite();
            }
        }


        // スプライト読み込み
        private async UniTask<Sprite> LoadSpriteAsync(string resourcePath, CancellationToken token = default) {
            token.ThrowIfCancellationRequested();

            Debug_.Log($"Load resource from :{resourcePath}", Colors.White);
            
            var sprite = await Resources.LoadAsync<Sprite>(resourcePath) as Sprite;

            // 仮に3秒程かかるとする
            await UniTask.WaitForSeconds(1f, cancellationToken: token);

            Debug_.Log($"Complete Load ");
            return sprite;
        }

        private void ResetImageSprite() {
            // 現在のリソースを解放
            if (_image.sprite != null) {
                Resources.UnloadAsset(_image.sprite);
                _image.sprite = null;
            }

            // デフォルト画像を設定
            _image.sprite = _defaultSprite;
        }


        // インデックスを移動
        private void MoveIndex(int direction) {
            int newIndex = Mathf.Clamp(_currentIndex.Value + direction, 0, ResourceKeys.Length - 1);
            _currentIndex.Value = newIndex;
        }
    }
}
