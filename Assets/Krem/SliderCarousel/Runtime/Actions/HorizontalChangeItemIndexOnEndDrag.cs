using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Providers;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Horizontal")] 
    public class HorizontalChangeItemIndexOnEndDrag : CoreAction
    {
        [InjectComponent] private Slider _slider;
        [InjectComponent] private DragProvider _dragProvider;

        protected override bool Action()
        {
            if (_slider.Active == false)
            {
                return false;
            }
            
            float delta = _dragProvider.dragHandler.StartPosition.x - _dragProvider.dragHandler.EndPosition.x;
            
            if (Mathf.Abs(delta) < _slider.changeIndexThreshold)
            {
                return false;
            }

            _slider.SelectedItemIndex += delta > 0 ? 1 : -1;
            
            return true;
        }
    }
}