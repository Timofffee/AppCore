using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;

namespace Krem.SomeFX.Actions
{
    [NodeGraphGroupName("Some FX")] 
    public class SetCameraBGColor : CoreAction 
    {
        public InputComponent<Components.ColorComponent> ColorComponent;
        public InputComponent<CameraLink> CameraLink;
        
        protected override bool Action()
        {
            CameraLink.Component.Camera.backgroundColor = ColorComponent.Component.color;
        
            return true;
        }
    }
}