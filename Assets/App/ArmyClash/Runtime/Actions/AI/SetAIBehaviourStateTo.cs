using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class SetAIBehaviourStateTo : CoreAction 
    {
        [ActionParameter] public bool State = true;
        
        public InputComponent<AIBehaviour> AIBehaviour;
        
        protected override bool Action()
        {
            AIBehaviour.Component.Active = State;
        
            return true;
        }
    }
}