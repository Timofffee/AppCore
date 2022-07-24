using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Rubber;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")]
    public class PointerDown : CoreAction
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
            
            if (_rubberControl.rubberPosition == RubberPosition.FloatingFixed)
                _rubberControl.Body.anchoredPosition = touchPosition;
            
            _rubberControl.Handle.anchoredPosition = _rubberControl.Body.anchoredPosition;
            _rubberControl.Axis = Vector2.zero;

            return true;
        }
    }
}