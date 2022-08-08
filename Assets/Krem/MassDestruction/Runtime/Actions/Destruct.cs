
using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")]
    public class Destruct : CoreAction
    {
        [InjectComponent] private Components.Destructible _destructible;
        
        protected override bool Action()
        {
            _destructible.solid.DoDisable();
            _destructible.fractured.DoEnable();
            
            SyncFracturedWithSolid();

            return true;
        }

        private void SyncFracturedWithSolid()
        {
            _destructible.fractured.Transform.position =
                _destructible.solid.Transform.position;
            
            _destructible.fractured.Transform.rotation =
                _destructible.solid.Transform.rotation;
            
            _destructible.fractured.Rigidbodies.ForEach(body =>
            {
                body.velocity = _destructible.solid.Rigidbody.velocity;
            });
        }
    }
}