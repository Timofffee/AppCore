using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Actions.AI
{
    [NodeGraphGroupName("ArmyClash/AI")] 
    public class LimitMovementSpeed : CoreAction 
    {
        public InputComponent<AIFilteredCollection> AICollection;
        public InputComponent<AIMovementSystem> AIMovementSystem;
        
        protected override bool Action()
        {
            if (AIMovementSystem.Component.Active == false)
            {
                return false;
            }
            
            AICollection.Component.FilteredAIBehaviours.ForEach(behaviour =>
            {
                Vector3 movementVelocity = behaviour.Rigidbody.velocity;
                if (movementVelocity.sqrMagnitude > Mathf.Pow(behaviour.UnitModel.CurrentUnitModel.MoveSpeed, 2))
                {
                    movementVelocity = movementVelocity.normalized * behaviour.UnitModel.CurrentUnitModel.MoveSpeed;
                }
                behaviour.Rigidbody.velocity = movementVelocity;
            });
        
            return true;
        }
    }
}