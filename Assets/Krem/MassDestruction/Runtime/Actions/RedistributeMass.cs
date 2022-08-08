using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.MassDestruction.Components;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")] 
    public class RedistributeMass : CoreAction
    {
        [InjectComponent] private Destructible _destructible;
        [InjectComponent] private DestructibleMass _destructibleMass;

        protected override bool Action()
        {
            _destructible.solid.Rigidbody.mass = _destructibleMass.mass;
            float redistributedMass = _destructibleMass.mass / _destructible.fractured.Rigidbodies.Count;
            
            _destructible.fractured.Rigidbodies.ForEach(rb =>
            {
                rb.mass = redistributedMass;
            });
            
            return true;
        }
    }
}