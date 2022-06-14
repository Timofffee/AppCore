using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.Rigidbody
{
    [NodeGraphGroupName("Jet Pack/Basis/Rigidbody")] 
    public class SetRigidbodyIsKinematicTo : CoreAction
    {
        [InjectComponent] private UnityEngine.Rigidbody _rigidbody;

        [ActionParameter] public bool isKinematic = true;
        
        protected override bool Action()
        {
            _rigidbody.isKinematic = isKinematic;
        
            return true;
        }
    }
}