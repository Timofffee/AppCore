using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.MassDestruction.Components;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")] 
    public class DestroyFracturedPartOnCollide : CoreAction
    {
        [InjectComponent] private CollideWithFracturePart _collideWithFracturePart;
        
        protected override bool Action()
        {
            _collideWithFracturePart.CollidedFracturePart.DestroyRequest();
        
            return true;
        }
    }
}