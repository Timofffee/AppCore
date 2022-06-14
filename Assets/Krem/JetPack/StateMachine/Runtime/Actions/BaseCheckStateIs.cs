using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.StateMachine.Components;
using Krem.JetPack.StateMachine.Scriptables;

namespace Krem.JetPack.StateMachine.Actions
{
    public abstract class BaseCheckStateIs<TStateMachine, TScriptableState, TState> : CoreAction
        where TStateMachine : BaseStateMachine<TScriptableState, TState>
        where TScriptableState : BaseScriptableState<TState> 
        where TState : Enum
    {
        [ActionParameter] public TState State;
        public InputComponent<TStateMachine> StateMachine;
        
        protected override bool Action()
        {
            if (StateMachine.Component.ScriptableState.State.Equals(State) == false)
            {
                return false;
            }
        
            return true;
        }
    }
}