using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.HyperControls.Actions.Joystick
{
    [NodeGraphGroupName("Jet Pack/HyperControls/Joystick")]
    public class HideBody : CoreAction
    {
        [InjectComponent] private Components.Joystick.Joystick _joystick;
        
        protected override bool Action()
        {
            _joystick.Body.gameObject.SetActive(false);
        
            return true;
        }
    }
}