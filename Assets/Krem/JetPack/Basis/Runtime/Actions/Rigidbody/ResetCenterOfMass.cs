using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.Rigidbody
{
    [NodeGraphGroupName("Jet Pack/Basis/Rigidbody")] 
    public class ResetCenterOfMass : CoreAction 
    {
        [InjectComponent] private UnityEngine.Rigidbody _rigidbody;
        
        protected override bool Action()
        {
            _rigidbody.ResetCenterOfMass();
        
            return true;
        }
    }
}