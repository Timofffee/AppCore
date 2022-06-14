using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Joystick;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Joystick
{
    [NodeGraphGroupName("Jet Pack/HyperControls/Joystick")]
    public class PointerDown : CoreAction
    {
        [InjectComponent] private Components.Joystick.Joystick _joystick;

        protected override bool Action()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystick.RectTransform, 
                _joystick.PointerEventData.position, 
                _joystick.CanvasCamera, 
                out Vector2 touchPosition
                );
            
            if (_joystick.joystickPosition == JoystickPosition.Floating || _joystick.joystickPosition == JoystickPosition.FloatingFixed)
                _joystick.Body.anchoredPosition = touchPosition;
            
            _joystick.Handle.anchoredPosition = _joystick.Body.anchoredPosition;
            _joystick.Axis = Vector2.zero;

            return true;
        }
    }
}