using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Components;
using Krem.JetPack.StateMachine.Scriptables;
using Krem.JetPack.StateMachine.States;

namespace Krem.JetPack.StateMachine.Actions
{
    [NodeGraphGroupName("Jet Pack/State Machine")] 
    public class AppCheckStateIs : BaseCheckStateIs<AppStateMachine, AppScriptableState, AppState>
    {
    }
}