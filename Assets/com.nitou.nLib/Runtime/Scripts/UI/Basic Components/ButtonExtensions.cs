using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UniRx;

// [参考]
//  コガネブログ: uGUI でスクリプトからボタンを押す方法 https://baba-s.hatenablog.com/entry/2018/01/03/110100
//  UnityQuesion: How to trigger a button click from script https://answers.unity.com/questions/945299/how-to-trigger-a-button-click-from-script.html
//  Hatena: ボタンを 1回だけ実行するようにする 拡張メソッド https://hacchi-man.hatenablog.com/entry/2020/12/12/220000

namespace Nitou {

    /// <summary>
    /// Buttonの拡張メソッドクラス
    /// </summary>
    public static partial class ButtonExtensions {

        /// ----------------------------------------------------------------------------
        // ボタンの実行

        /// <summary>
        /// 指定したボタンイベントを実行する拡張メソッド
        /// </summary>
        public static void Execute<T>(this Button @this, ExecuteEvents.EventFunction<T> eventFunctor)
            where T : IEventSystemHandler {

            ExecuteEvents.Execute(
                    target: @this.gameObject,
                    eventData: new PointerEventData(EventSystem.current),   // ※currentで現在のイベントシステムを取得できる
                    functor: eventFunctor
                );
        }


        /// ----------------------------------------------------------------------------
        // イベントの登録

        /// <summary>
        /// イベントを登録する拡張メソッド
        /// </summary>
        //public static IDisposable SetOnClickDestination(this Button @this, Action onClick) {
        //    return @this.onClick
        //        .AsObservable()
        //        .Subscribe(x => onClick.Invoke())
        //        .AddTo(@this);
        //}

        /// <summary>
        /// １回だけ実行させるイベントを登録する拡張メソッド
        /// </summary>
        public static void OneShot(this Button self, Action action) {
            self.onClick.AddListener(() => {
                action?.Invoke();
                self.onClick.RemoveAllListeners();     // ※全消去であることに注意
            });
        }
    }


}