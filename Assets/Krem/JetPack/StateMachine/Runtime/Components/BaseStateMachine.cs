using System;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Ports;
using Krem.JetPack.StateMachine.Scriptables;
using UnityEngine;

namespace Krem.JetPack.StateMachine.Components
{
    public abstract class BaseStateMachine<TScriptableState, TState> : CoreComponent 
        where TScriptableState : BaseScriptableState<TState> 
        where TState : Enum
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected TScriptableState _scriptableState;

        [Header("Ports")]
        public OutputSignal OnStateChanged;

        public TScriptableState ScriptableState => _scriptableState;
        
        private void OnEnable()
        {
            _scriptableState.OnStateChanged += ChangeStateHandler;
        }

        private void OnDisable()
        {
            _scriptableState.OnStateChanged -= ChangeStateHandler;
        }

        private void ChangeStateHandler()
        {
            OnStateChanged.Invoke();
        }
    }
}