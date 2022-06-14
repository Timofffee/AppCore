using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.GameObject
{
    [NodeGraphGroupName("Jet Pack/Basis/GameObject")]
    public class DestroyGameObject : CoreAction
    {
        [InjectComponent] private UnityEngine.Transform _transform;
        
        [ActionParameter] public bool DestroyImmediate = false;
        
        protected override bool Action()
        {
            if (DestroyImmediate)
            {
                UnityEngine.GameObject.DestroyImmediate(_transform.gameObject);
            }
            else
            {
                UnityEngine.GameObject.Destroy(_transform.gameObject);
            }
            
            return true;
        }
    }
}