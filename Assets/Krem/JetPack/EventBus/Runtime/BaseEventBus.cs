using System;
using UnityEngine;

namespace Krem.JetPack.EventBus
{
    public abstract class BaseEventBus : ScriptableObject
    {
        protected event Action _event;

        public virtual void Invoke()
        {
            _event?.Invoke();
        }
        
        public virtual void AddListener(Action listener)
        {
            _event += listener;
        }

        public virtual void RemoveListener(Action listener)
        {
            _event -= listener;
        }
    }

    public abstract class BaseEventBus<T> : BaseEventBus
    {
        public T value;
        
        public virtual void Invoke(T eventValue)
        {
            value = eventValue;
            Invoke();
        }
    }
}