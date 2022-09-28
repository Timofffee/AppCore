using System.Linq;
using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class SearchClosestTarget : CoreAction
    {
        public InputComponent<AIFilteredCollection> SourceAICollection;
        public InputComponent<AIFilteredCollection> TargetsAICollection;
        
        protected override bool Action()
        {
            if (SourceAICollection.Component.FilteredAIBehaviours.Count == 0
                || TargetsAICollection.Component.FilteredAIBehaviours.Count == 0)
            {
                return false;
            }
            
            SourceAICollection.Component.FilteredAIBehaviours.ForEach(ai =>
            {
                if (ai.AITarget != null && ai.AITarget.Active)
                {
                    return;
                }
                
                ai.AITarget = TargetsAICollection.Component.FilteredAIBehaviours
                    .OrderByDescending(target => (ai.Transform.position - target.Transform.position).sqrMagnitude).ToList().Last();
            });
        
            return true;
        }
    }
}