using UnityEngine;
using UnityEngine.Pool;

// [REF]
//  qiita: Unity標準のObjectPoolを汎用的に使うクラスの作成 https://qiita.com/KeichiMizutani/items/ca46a40de02e87b3d8a8

namespace Nitou.DesignPattern.Pooling {

    /// <summary>
    /// 
    /// </summary>
    public abstract class PoolManager<T> : MonoBehaviour
        where T : MonoBehaviour, IPooledObject<T> {

        [SerializeField] private T _pooledPrefab;
        protected IObjectPool<T> _objectPool;

        [SerializeField] private bool _collectionCheck = true;

        [SerializeField] int _defaultCapacity = 32;
        [SerializeField] private int _maxSize = 100;


        /// ----------------------------------------------------------------------------
        // Public Method

        public virtual void Initialize() {
            _objectPool = new ObjectPool<T>(
                createFunc: Create,
                actionOnGet: OnBeforeRent,
                actionOnRelease: OnBeforeReturn,
                actionOnDestroy: OnDestroyPooledObject,
                _collectionCheck, 
                _defaultCapacity, 
                _maxSize);
        }


        /// ----------------------------------------------------------------------------
        // Protected Method

        protected virtual T Create() {
            T instace = Instantiate<T>(_pooledPrefab, transform.position, Quaternion.identity, transform);
            instace.ObjectPool = _objectPool;
            return instace;
        }

        protected virtual void OnBeforeRent(T pooledObject) {
            pooledObject.gameObject.SetActive(true);
        }

        protected virtual void OnBeforeReturn(T pooledObject) {
            pooledObject.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyPooledObject(T pooledObject) {
            pooledObject.Deactivate();
            Destroy(pooledObject.gameObject);
        }
    }
}
