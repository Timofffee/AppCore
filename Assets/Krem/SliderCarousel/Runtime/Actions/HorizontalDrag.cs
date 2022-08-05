using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.Basis.Components.Providers;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Horizontal")] 
    public class HorizontalDrag : CoreAction
    {
        [InjectComponent] private DragProvider _dragProvider;
        [InjectComponent] private TransformLink _transformComponent;
        [InjectComponent] private Slider _slider;
        
        protected override bool Action()
        {
            if (_dragProvider.dragHandler.IsDragging == false || _slider.Active == false)
            {
                return false;
            }

            Vector3 position = _transformComponent.Transform.localPosition;
            position.x += _dragProvider.dragHandler.PointerEventData.delta.x * _slider.dragSensitivity;
            _transformComponent.Transform.localPosition = position;
        
            return true;
        }
    }
}