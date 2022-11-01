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
                Vector3 movementForce = Vector3.zero;
                
                if (behaviour.AITarget != null && behaviour.AITarget.Active)
                {
                    movementForce = behaviour.AITarget.Transform.position - behaviour.Transform.position;
                }

                behaviour.Rigidbody.AddForce((movementForce + behaviour.RandomDirection) * DeltaTime.Data * AIMovementSystem.Component.AccelerationSpeed);
            });
        
            return true;
        }
    }
}