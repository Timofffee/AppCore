using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.MassDestruction.Actions
{
    [NodeGraphGroupName("Mass Destruction")]
    public class DestructTouched : CoreAction
    {
        [InjectComponent] private Components.TouchDestructor _touchDestructor;
        
        protected override bool Action()
        {
            _touchDestructor.TouchedDestructible.Destruct();
            
            return true;
        }
    }
}