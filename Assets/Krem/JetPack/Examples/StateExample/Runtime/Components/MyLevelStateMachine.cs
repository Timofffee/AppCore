using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Components;
using StateExample.Enums;
using StateExample.Scriptables;
using UnityEngine;

namespace StateExample.Components
{
    [NodeGraphGroupName("State Example")]
    public class MyLevelStateMachine : BaseStateMachine<ScriptableMyLevelState, MyLevelStates>
    {    
    
    }
}