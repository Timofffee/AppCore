using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Actions;
using StateExample.Components;
using StateExample.Enums;
using StateExample.Scriptables;

namespace StateExample.Actions
{
    [NodeGraphGroupName("State Example")] 
    public class SetStateTo : BaseSetStateTo<MyLevelStateMachine, ScriptableMyLevelState, MyLevelStates>
    {

    }
}