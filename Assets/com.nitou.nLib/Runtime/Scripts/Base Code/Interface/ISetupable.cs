
// [Memo]
//  任意のタイミングでリセットしたいことが多くあるため，インターフェースを作成．
//  記述の統一を主目的としているため，ポリモーフィックな使用は未想定．

namespace Nitou {

    public interface ISetupable {
        public void Setup();
        public void Teardown();
    }

    public interface ISetupable<T> {
        public void Setup(T item);
        public void Teardown();
    }

    public interface ISetupable<T1, T2> {
        public void Setup(T1 item1, T2 item2);
        public void Teardown();
    }
}