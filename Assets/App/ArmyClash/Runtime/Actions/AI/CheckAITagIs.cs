using App.ArmyClash.Components.AI;
using App.ArmyClash.Components.Events;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class CheckAITagIs : CoreAction
    {
        [ActionParameter] public AITagType AITagType;

        public InputComponent<AIBehaviourEventBusProvider> AIBehaviourEventBusProvider;
        
        protected override bool Action()
        {
            if (AIBehaviourEventBusProvider.Component.EventBus.value.AITagType != AITagType)
            {
                return false;
            }
        
            return true;
        }
    }
}