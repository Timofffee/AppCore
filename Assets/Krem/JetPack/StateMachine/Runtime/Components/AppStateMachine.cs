using Krem.AppCore.Attributes;
using Krem.JetPack.StateMachine.Scriptables;
using Krem.JetPack.StateMachine.States;

namespace Krem.JetPack.StateMachine.Components
{
    [NodeGraphGroupName("Jet Pack/State Machine")]
    public class AppStateMachine : BaseStateMachine<AppScriptableState, AppState>
    {
    }
}