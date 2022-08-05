using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.Basis.Components.Providers;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Radial")] 
    public class RadialDrag : CoreAction
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

            Vector3 rotation = Vector3.zero;
            rotation.y = _dragProvider.dragHandler.PointerEventData.delta.x * _slider.dragSensitivity;
            _transformComponent.Transform.Rotate(-rotation, Space.Self);
            
            return true;
        }
    }
}