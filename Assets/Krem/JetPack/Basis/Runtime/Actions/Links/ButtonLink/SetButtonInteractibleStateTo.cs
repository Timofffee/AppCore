using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace Krem.JetPack.Basis.Actions.Links.ButtonLink
{
    [NodeGraphGroupName("Jet Pack/Basis/Links/ButtonLink")] 
    public class SetButtonInteractableStateTo : CoreAction
    {
        public InputComponent<Components.Links.ButtonLink> ButtonLink;
        
        [ActionParameter] public bool state = false;
        
        protected override bool Action()
        {
            ButtonLink.Component.Button.interactable = state;
        
            return true;
        }
    }
}