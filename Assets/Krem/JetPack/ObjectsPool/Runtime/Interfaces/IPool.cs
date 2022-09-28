using System.Collections.Generic;

namespace Krem.JetPack.ObjectsPool.Interfaces
{
    public interface IPool<T> where T : IPoolItem
    {
        public T GetFromPool();
        public void ReturnToPool(T item);
        public Queue<T> PoolQueue { get; }
        public List<T> PoolItems { get; }

        public void InstantiatePool();
        public abstract void ClearPool();
        
        public void ExpandPool(int size);
    }
}