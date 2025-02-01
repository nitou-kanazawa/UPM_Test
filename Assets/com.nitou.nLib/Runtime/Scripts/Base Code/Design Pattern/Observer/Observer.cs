using System;
using System.Collections.Generic;
using UnityEngine;

// [REF]
//  qiita: 作りながら理解するUniRx https://qiita.com/mattak/items/106dfd0974653aa06fbc#5-observable

namespace Nitou.DesignPattern.Observer {

    // 監視者
    public interface IObserver<T> {

        // データが来た
        void OnNext(T value);

        // エラーが来た
        void OnError(Exception error);

        // データはもう来ない
        void OnComplete();
    }

    // 監視可能であることを示す
    public interface IObservable<T> {

        // 監視者が購読する
        IDisposable Subscribe(IObserver<T> observer);
    }


    // 監視対象
    public interface ISubject<T> : IObserver<T>, IObservable<T> { }


    /// <summary>
    /// 
    /// </summary>
    public class Subject<T> : ISubject<T> {

        private List<IObserver<T>> observers = new();

        #region IObserver
        public void OnNext(T next) {
            foreach (var observer in this.observers) {
                observer.OnNext(next);
            }
        }

        public void OnError(Exception error) {
            foreach (var observer in this.observers) {
                observer.OnError(error);
            }

            this.observers.Clear();
        }

        public void OnComplete() {
            foreach (var observer in this.observers) {
                observer.OnComplete();
            }

            this.observers.Clear();
        }
        #endregion

        #region IObservable
        public IDisposable Subscribe(IObserver<T> observer) {
            this.observers.Add(observer);
            // 購読管理のclassを返す
            return new Subscription(this, observer);
        }
        #endregion

        private void Unsubscribe(IObserver<T> observer) {
            this.observers.Remove(observer);
        }


        // 購読管理をするclass. Dispose()を呼ぶことで購読をやめる
        class Subscription : IDisposable {

            private IObserver<T> _observer;
            private Subject<T> _subject;

            public Subscription(Subject<T> subject, IObserver<T> observer) {
                this._subject = subject;
                this._observer = observer;
            }

            public void Dispose() {
                this._subject.Unsubscribe(this._observer);
            }
        }
    }


    public class Observable<T> : IObservable<T> {

        // 処理内容をデリゲートで保持するため，Coldな振る舞いをする
        private Func<IObserver<T>, IDisposable> creator;


        private Observable(Func<IObserver<T>, IDisposable> creator) {
            this.creator = creator;
        }

        public IDisposable Subscribe(IObserver<T> observer) {
            // Subscribeした瞬間に関数を実行するのが特徴
            return this.creator(observer);
        }

        // Observableを直接渡したくないため、Createメソッドを作っておく.
        public static IObservable<T> Create(Func<IObserver<T>, IDisposable> creator) {
            return new Observable<T>(creator);
        }
    }


    public class Observer<T> : IObserver<T> {

        private Action<T> _next;
        private Action<Exception> _error;
        private Action _complete;


        private Observer(Action<T> next, Action<Exception> error, Action complete) {
            _next = next;
            _error = error;
            _complete = complete;
        }


        public void OnNext(T value) {
            _next?.Invoke(value);
        }

        public void OnError(Exception error) {
            _error?.Invoke(error);
        }

        public void OnComplete() {
            _complete?.Invoke();
        }


        public static IObserver<T> Create(Action<T> next, Action<Exception> error, Action complete) {
            return new Observer<T>(next, error, complete);
        }
    }


    // 購読解除するつもりがないときに返す Disposable
    public class EmptyDisposable : IDisposable {
        public void Dispose() { }
    }

    // 複数の講読をいっぺんに解除するための Disposable
    public class CollectionDisposable : IDisposable {
        
        private IList<IDisposable> _disposables;

        public CollectionDisposable(IList<IDisposable> disposables) {
            this._disposables = disposables;
        }

        public void Dispose() {
            foreach (var disposable in this._disposables) {
                disposable.Dispose();
            }
        }
    }





    namespace Demo {

        internal class TestMain {

            public void Main() {

                var observable = Observable<string>.Create(observer => {

                    // ネタが回答出来たら握って提供
                    Debug.Log("ネタを解凍します");
                    observer.OnNext("ぶり");
                    observer.OnComplete();
                    return new EmptyDisposable();
                });

            }


        }


    }

}
