using System;
using UnityEngine;

namespace Krem.JetPack.StateMachine.Scriptables
{
    public abstract class BaseScriptableState<T> : ScriptableObject where T : Enum
    {
        [SerializeField] protected T _state;

        public event Action OnStateChanged;

        public T State
        {
            get => _state;
            set
            {
                _state = value;
                
                OnStateChanged?.Invoke();
            }
        }
    }
}
