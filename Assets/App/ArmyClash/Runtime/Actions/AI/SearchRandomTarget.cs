using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Extensions;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class SearchRandomTarget : CoreAction
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
                ai.AITarget = TargetsAICollection.Component.FilteredAIBehaviours.Random();
            });
        
            return true;
        }
    }
}