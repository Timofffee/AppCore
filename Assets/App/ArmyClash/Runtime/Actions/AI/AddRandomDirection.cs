using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class AddRandomDirection : CoreAction
    {
        [ActionParameter] public Vector3 RandomRange; 
        
        public InputComponent<AIFilteredCollection> AICollection;
        
        protected override bool Action()
        {
            AICollection.Component.FilteredAIBehaviours.ForEach(behaviour =>
            {
                if (behaviour.Active == false)
                {
                    return;
                }
                
                behaviour.RandomDirection = new Vector3(
                    Random.Range(-RandomRange.x, RandomRange.x),
                    0,
                    Random.Range(-RandomRange.z, RandomRange.z)
                );
            });
        
            return true;
        }
    }
}