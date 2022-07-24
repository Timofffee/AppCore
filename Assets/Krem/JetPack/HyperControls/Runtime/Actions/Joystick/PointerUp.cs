using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Joystick
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Joystick")]
    public class PointerUp : CoreAction
    {
        [InjectComponent] private Components.Joystick.Joystick _joystick;

        protected override bool Action()
        {
            _joystick.Handle.anchoredPosition = _joystick.Body.anchoredPosition;
            
            _joystick.Axis = Vector2.zero;

            return true;
        }
    }
}