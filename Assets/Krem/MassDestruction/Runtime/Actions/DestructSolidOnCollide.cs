using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.MassDestruction.Components;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")] 
    public class DestructSolidOnCollide : CoreAction
    {
        [InjectComponent] private CollideWithSolid _collideWithSolid;
        
        protected override bool Action()
        {
            _collideWithSolid.CollidedSolid.Destructible.Destruct();
        
            return true;
        }
    }
}