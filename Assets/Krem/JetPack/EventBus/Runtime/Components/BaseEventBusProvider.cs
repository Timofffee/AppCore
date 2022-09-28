using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.EventBus.Components
{
    public abstract class BaseEventBusProvider<T> : CoreComponent where T : BaseEventBus
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private T _eventBus;

        [Header("Ports")]
        [BindInputSignal(nameof(InvokeEvent))] public InputSignal CallInvoke;
        public OutputSignal OnHandle;

        public T EventBus => _eventBus;

        private void OnEnable()
        {
            _eventBus.AddListener(Handler);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(Handler);
        }

        public virtual void InvokeEvent()
        {
            _eventBus.Invoke();
        }

        protected virtual void Handler()
        {
            OnHandle.Invoke();
        }
    }
    
    public abstract class BaseEventBusProvider<T, V> : CoreComponent where T : BaseEventBus<V>
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private T _eventBus;
        [SerializeField, NotNull] private V _value;

        [Header("Ports")]
        [BindInputSignal(nameof(InvokeEvent))] public InputSignal CallInvoke;
        public OutputSignal OnHandle;

        public T EventBus => _eventBus;

        private void OnEnable()
        {
            _eventBus.AddListener(Handler);
        }

        private void OnDisable()
        {
            _eventBus.RemoveListener(Handler);
        }

        public virtual void InvokeEvent()
        {
            _eventBus.value = _value;
            _eventBus.Invoke();
        }

        protected virtual void Handler()
        {
            OnHandle.Invoke();
        }
    }
}
