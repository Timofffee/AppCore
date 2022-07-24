using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.HyperControls.Actions.Joystick
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Joystick")]
    public class ShowHandle : CoreAction
    {
        [InjectComponent] private Components.Joystick.Joystick _joystick;
        
        protected override bool Action()
        {
            _joystick.Handle.gameObject.SetActive(true);
        
            return true;
        }
    }
}