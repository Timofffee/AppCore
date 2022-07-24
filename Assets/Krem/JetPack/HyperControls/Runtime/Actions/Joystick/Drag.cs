using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Joystick;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Joystick
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Joystick")]
    public class Drag : CoreAction
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
            
            Vector2 moveVector = touchPosition - _joystick.Body.anchoredPosition;
            Vector2 stickOffset = moveVector;
            
            // Animate Stick
            if (stickOffset.magnitude > _joystick.Radius)
            {
                stickOffset = stickOffset.normalized * _joystick.Radius;
                
                if (_joystick.joystickPosition == JoystickPosition.Floating)
                    _joystick.Body.anchoredPosition = touchPosition - stickOffset;
            }

            _joystick.Handle.anchoredPosition = _joystick.Body.anchoredPosition + stickOffset;
            
            // Write Horizontal and Vertical values to Attribute
            moveVector /= _joystick.Radius;
            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }
            
            _joystick.Axis = moveVector;
            
            return true;
        }

        public void MoveJoystick()
        {
            // if (joystickAttribute.joystickPosition == JoystickPosition.Floating)
            //     joystickAttribute.body.anchoredPosition = touchPosition;
        }
    }
}