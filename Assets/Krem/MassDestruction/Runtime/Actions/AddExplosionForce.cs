using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.MassDestruction.Components;
using UnityEngine;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")]
    public class AddExplosionForce : CoreAction 
    {
        [InjectComponent] private Destructible _destructible;
        [InjectComponent] private ExplosionForce _explosionForce;
        
        protected override bool Action()
        {
            _destructible.fractured.Rigidbodies.ForEach(body =>
            {
                body.AddExplosionForce(
                    _explosionForce.force, 
                    _destructible.fractured.Transform.position, 
                    _explosionForce.radius, 
                    _explosionForce.upwardsModifier, 
                    ForceMode.Impulse
                    );
            });

            return true;
        }
    }
}