using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.ObjectsPool.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Components
{
    [NodeGraphHidden]
    public abstract class BasePool<T> : CoreComponent, IPool<T> where T : IPoolItem
    {    
        [Header("Pool Settings")]
        [SerializeField] protected int _poolSize;
        
        [Header("Pool Items Only for view")]
        [SerializeField] private List<T> _poolItems;

        public List<T> PoolItems => _poolItems;
        public Queue<T> PoolQueue { get; } = new Queue<T>();

        public abstract T InstantiatePoolObject();

        public void InstantiatePool()
        {
            _poolItems = new List<T>(_poolSize);

            for (int i = 0; i < _poolSize; i++)
            {
                T poolObject = InstantiatePoolObject();
                AddObjectToPool(poolObject);
            }
        }

        public abstract void ClearPool();

        public void ExpandPool(int size)
        {
            for (int i = 0; i < size; i++)
            {
                T poolObject = InstantiatePoolObject();
                AddObjectToPool(poolObject);
            }
        }

        public virtual T GetFromPool()
        {
            if (PoolQueue.Count == 0)
            {
                ExpandPool(_poolSize);
            }
            
            return PoolQueue.Dequeue();
        }

        public virtual void ReturnToPool(T item)
        {
            PoolQueue.Enqueue(item);
        }
        
        private void AddObjectToPool(T item)
        {
            _poolItems.Add(item);
            PoolQueue.Enqueue(item);
        }
    }
}