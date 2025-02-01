using UnityEngine;
using UnityEngine.Pool;

// [REF]
//  qiita: Unity標準のObjectPoolを汎用的に使うクラスの作成 https://qiita.com/KeichiMizutani/items/ca46a40de02e87b3d8a8
//  github: game-programming-patterns-demo https://github.com/Unity-Technologies/game-programming-patterns-demo

namespace Nitou.DesignPattern.Pooling{

    /// <summary>
    /// プールされるオブジェクトのインターフェース
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPooledObject<T> where T : class {

        public IObjectPool<T> ObjectPool { set; }
        
        public void Initialize();
        
        public void Deactivate();
    }

}
