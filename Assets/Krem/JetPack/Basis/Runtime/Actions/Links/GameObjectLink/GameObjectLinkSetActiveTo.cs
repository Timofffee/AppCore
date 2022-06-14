using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace Krem.JetPack.Basis.Actions.Links.GameObjectLink
{
    [NodeGraphGroupName("Jet Pack/Basis/Links/GameObjectLink")] 
    public class GameObjectSetActiveTo : CoreAction
    {
        public InputComponent<Components.Links.GameObjectLink> GameObjectLink;
        
        [ActionParameter] public bool state = false;
        
        protected override bool Action()
        {
            GameObjectLink.Component.GameObject.SetActive(state);
        
            return true;
        }
    }
}