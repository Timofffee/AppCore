using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Rubber;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")]
    public class Drag : CoreAction
    {
        [InjectComponent] private RubberControl _rubberControl;

        protected override bool Action()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rubberControl.RectTransform, 
                _rubberControl.PointerEventData.position, 
                _rubberControl.CanvasCamera, 
                out Vector2 touchPosition
                );
            
            Vector2 moveVector = touchPosition - _rubberControl.Body.anchoredPosition;
            Vector2 stickOffset = moveVector;
            
            // Animate Stick
            if (stickOffset.magnitude > _rubberControl.Radius)
            {
                stickOffset = stickOffset.normalized * _rubberControl.Radius;
            }

            _rubberControl.Handle.anchoredPosition = _rubberControl.Body.anchoredPosition + stickOffset;
            
            // Write Horizontal and Vertical values to Attribute
            moveVector /= _rubberControl.Radius;
            if (moveVector.magnitude > 1)
            {
                moveVector = moveVector.normalized;
            }
            
            _rubberControl.Axis = moveVector;
            
            return true;
        }

        public void MoveJoystick()
        {
            // if (joystickAttribute.joystickPosition == JoystickPosition.Floating)
            //     joystickAttribute.body.anchoredPosition = touchPosition;
        }
    }
}