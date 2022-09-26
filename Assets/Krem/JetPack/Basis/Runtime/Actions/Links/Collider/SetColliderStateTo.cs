using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;

namespace Krem.JetPack.Basis.Actions.Links.Collider
{
    [NodeGraphGroupName("Jet Pack/Basis/Links/ColliderLink")] 
    public class SetColliderStateTo : CoreAction
    {
        public InputComponent<ColliderLink> ColliderLink;
        
        [ActionParameter] public bool state = false;
        
        protected override bool Action()
        {
            ColliderLink.Component.Collider.enabled = state;
        
            return true;
        }
    }
}