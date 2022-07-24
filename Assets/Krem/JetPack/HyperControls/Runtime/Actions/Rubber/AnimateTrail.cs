using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Rubber;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")] 
    public class AnimateTrail : CoreAction 
    {
        [InjectComponent] private RubberControl _rubberControl;

        private Vector2 _moveVector;
        private float _offset = 0f;
        private Vector3 _rotation = Vector3.zero;

        protected override bool Action()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rubberControl.RectTransform, 
                _rubberControl.PointerEventData.position, 
                _rubberControl.CanvasCamera, 
                out Vector2 touchPosition
            );
            
            _moveVector = _rubberControl.Handle.anchoredPosition - _rubberControl.Center.anchoredPosition;
            _offset = _moveVector.magnitude;

            _rubberControl.Trail.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _offset);
            _rubberControl.Trail.up = -_moveVector;
        
            return true;
        }
    }
}