using App.ArmyClash.StateMachines;
using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Components;

namespace App.ArmyClash.Components.StateMachine
{
    [NodeGraphGroupName("ArmyClash/StateMachine")]
    public class AppStateMachine : BaseStateMachine<AppScriptableState, AppStates>
    {
    }
}
