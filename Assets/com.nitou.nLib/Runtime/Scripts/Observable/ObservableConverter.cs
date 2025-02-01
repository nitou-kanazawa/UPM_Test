using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

// [REF]
//  qiita: UniTaskをCancellationTokenを指定しながらToObservableするメモ https://qiita.com/toRisouP/items/8ec18d73d9e8c5169587
//  _: 非同期コールバック関数パターン https://developer.aiming-inc.com/csharp/unity-csharp-async-callback-patterns/
//  _: UniRxで課題だったRxとasync/awaitの連携がR3では楽になった件 https://developer.aiming-inc.com/csharp/post-10773/

namespace Nitou {

    public static class ObservableConverter {

        /// <summary>
        /// UniTask.ToObservable() を<see cref="CancellationToken"/>を指定して行うためのメソッド．
        /// </summary>
        public static IObservable<T> FromUniTask<T>(Func<CancellationToken, UniTask<T>> func) {

            return Observable.Create<T>(observer => {

                // [NOTE] 外部に渡すcdでUniTaskを停止させる.
                var cd = new CancellationDisposable();

                UniTask.Void(async () => {
                    try {
                        observer.OnNext(await func(cd.Token));
                        observer.OnCompleted();
                    } catch (Exception e) {
                        observer.OnError(e);
                    }
                });
                return cd;
            });
        }

        /// <summary>
        /// UniTask.ToObservable() を<see cref="CancellationToken"/>を指定して行うためのメソッド．
        /// </summary>
        public static IObservable<Unit> FromUniTask(Func<CancellationToken, UniTask> func) {

            return Observable.Create<Unit>(observer => {

                // [NOTE] 外部に渡すcdでUniTaskを停止させる.
                var cd = new CancellationDisposable();

                UniTask.Void(async () => {
                    try {
                        await func(cd.Token);
                        observer.OnNext(Unit.Default);
                        observer.OnCompleted();
                    } catch (Exception e) {
                        observer.OnError(e);
                    }
                });
                return cd;
            });

        }
    }
}
