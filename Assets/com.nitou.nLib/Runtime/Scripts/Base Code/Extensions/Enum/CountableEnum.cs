using System;

// [参考]
//  ライブドアブログ: ジェネリクス型の比較方法 http://templatecreate.blog.jp/archives/30579779.html

namespace Nitou {

    /// <summary>
    /// 列挙型の要素順に意味を持たせるためのラッパー（Next,Previousへの遷移）
    /// </summary>
    public class CountableEnum<T> where T : Enum {
        
        private readonly Array _valueArray;  // 対象(列挙型)の全要素
        private int _id;            // 現在値のインデックス


        /// ----------------------------------------------------------------------------
        // Properity

        public Type Type { get => Get(0).GetType(); }
        public T Head { get => Get(0); }
        public T Tail { get => Get(_valueArray.Length - 1); }
        public T Current { get => Get(_id); }

        // 判定
        public bool IsHead { get => Get(_id).Equals(Head); }
        public bool IsTail { get => Get(_id).Equals(Tail); }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CountableEnum(T target) {
            _valueArray = Enum.GetValues(target.GetType());      // 列挙型の全要素
            _id = GetId(target);                             // 指定要素のインデックス
        }

        // 比較
        public bool Is(T target) => Get(_id).CompareTo(target) == 0;

        // 遷移
        public T MoveNext() {
            _id = IsTail ? 0 : _id + 1;   // 次の値に進める
            return Current;
        }
        public T MovePrevious() {
            _id = IsHead ? _valueArray.Length - 1 : _id - 1;   // 前の値に戻す
            return Current;
        }

        // デバッグ
        public override string ToString() {
            return string.Format("type:{0} [{1}-{2}]  current:{3}", Type, Head, Tail, Get(_id));
        }


        /// ----------------------------------------------------------------------------
        // Private Method
        
        private T Get(int id) => (T)_valueArray.GetValue(id);
        
        private int GetId(T key) {
            for (int i = 0; i < _valueArray.Length; i++) {
                var value = Get(i);
                if (key.Equals(value)) return i;
            }
            throw new System.ArgumentException();
        }
    }

}
