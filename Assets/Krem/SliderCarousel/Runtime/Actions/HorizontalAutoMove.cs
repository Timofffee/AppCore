using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Handlers;
using Krem.JetPack.Basis.Components.Links;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Horizontal")] 
    public class HorizontalAutoMove : CoreAction
    {
        [InjectComponent] private Slider _slider;
        [InjectComponent] private UpdateHandler _updateHandler;
        [InjectComponent] private TransformLink _transformComponent;
        
        [BindInputSignal(nameof(EnabledHandler))] public InputSignal Enable;
        [BindInputSignal(nameof(DisableHandler))] public InputSignal Disable;

        public OutputSignal OnPlaced;
        
        private bool _active = false;
        
        protected override bool Action()
        {
            if (_active == false || _slider.Active == false)
            {
                return false;
            }

            Vector3 position = _transformComponent.Transform.localPosition;

            if (Mathf.Abs(position.x + _slider.CurrentItem.TransformLink.Transform.localPosition.x) <= _slider.instantPlacementThreshold)
            {
                position.x = -_slider.CurrentItem.TransformLink.Transform.localPosition.x;
                _transformComponent.Transform.localPosition = position;
                _active = false;
                
                OnPlaced.Invoke();
                
                return true;
            }
            
            position.x = Mathf.Lerp(
                position.x, 
                -_slider.CurrentItem.TransformLink.Transform.localPosition.x, 
                _updateHandler.DeltaTime.Data * _slider.autoMoveSpeed
                );
            _transformComponent.Transform.localPosition = position;

            return true;
        }

        protected void EnabledHandler()
        {
            _active = true;
        }

        protected void DisableHandler()
        {
            _active = false;
        }
    }
}