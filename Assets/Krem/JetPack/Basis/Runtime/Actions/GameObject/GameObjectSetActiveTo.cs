using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.GameObject
{
    [NodeGraphGroupName("Jet Pack/Basis/GameObject")] 
    public class GameObjectSetActiveTo : CoreAction 
    {
        [InjectComponent] private UnityEngine.Transform _transform;
        
        [ActionParameter] public bool state = false;
        
        protected override bool Action()
        {
            _transform.gameObject.SetActive(state);
        
            return true;
        }
    }
}