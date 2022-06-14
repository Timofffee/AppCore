using System.Collections.Generic;
using Krem.AppCore.Attributes;
using Krem.JetPack.ObjectsPool.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Scriptables
{
    [NodeGraphHidden]
    public abstract class ScriptableBasePool<T> : ScriptableObject, IPool<T> where T : IPoolItem
    {    
        [Header("Pool Settings")]
        [SerializeField] protected int _poolSize;
        
        private List<T> _poolItems;

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

        public void AddObjectToPool(T item)
        {
            _poolItems.Add(item);
            PoolQueue.Enqueue(item);
        }

        public void ExpandPool(int size)
        {
            for (int i = 0; i < size; i++)
            {
                T poolObject = InstantiatePoolObject();
                AddObjectToPool(poolObject);
            }
        }

        public T GetFromPool()
        {
            if (PoolQueue.Count == 0)
            {
                ExpandPool(_poolSize);
            }
            
            return PoolQueue.Dequeue();
        }

        public void ReturnToPool(T item)
        {
            PoolQueue.Enqueue(item);
        }
    }
}