using App.ArmyClash.Components.StateMachine;
using App.ArmyClash.StateMachines;
using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Actions;

namespace App.ArmyClash.Actions.StateMachine
{
    [NodeGraphGroupName("ArmyClash/StateMachine")] 
    public class AppCheckStateIs : BaseCheckStateIs<AppStateMachine, AppScriptableState, AppStates>
    {
    }
}