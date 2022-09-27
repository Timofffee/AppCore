using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class AddMovementForce : CoreAction 
    {
        public InputComponent<AIFilteredCollection> AICollection;
        public InputComponent<AIMovementSystem> AIMovementSystem;
        public InputData<float> DeltaTime;
        
        protected override bool Action()
        {
            if (AIMovementSystem.Component.Active == false)
            {
                return false;
            }
            
            AICollection.Component.FilteredAIBehaviours.ForEach(behaviour =>
            {
                if (behaviour.AITarget == null)
                {
                    return;
                }
                
                Vector3 movementForce = behaviour.AITarget.Transform.position - behaviour.Transform.position;
                behaviour.Rigidbody.AddForce(movementForce * DeltaTime.Data * AIMovementSystem.Component.AccelerationSpeed);
            });
        
            return true;
        }
    }
}