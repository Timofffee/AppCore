using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Handlers;
using Krem.JetPack.Basis.Components.Links;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Radial")] 
    public class RadialAutoMove : CoreAction
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

            float angleBetweenPieces = 360f / _slider.Items.Count;
            float destinationAngle = angleBetweenPieces * _slider.SelectedItemIndex;
            
            float currentWrappedAngle = 180f + Vector3.SignedAngle(
                _transformComponent.Transform.forward, 
                Vector3.back, 
                Vector3.down
                );
            if (currentWrappedAngle == 360f)
            {
                currentWrappedAngle = 0;
            }

            float deltaAngle = destinationAngle - currentWrappedAngle;
            float deltaAngleInverted = destinationAngle - currentWrappedAngle + 360;
            float chosenAngle = Mathf.Abs(deltaAngle) > Mathf.Abs(deltaAngleInverted) ? deltaAngleInverted : deltaAngle;
            float updatedAngle = chosenAngle * _updateHandler.DeltaTime.Data * _slider.autoMoveSpeed;

            if (Mathf.Abs(updatedAngle) < _slider.instantPlacementThreshold)
            {
                updatedAngle = deltaAngle;
                _active = false;
                
                OnPlaced.Invoke();
            }

            _transformComponent.Transform.Rotate(0f, updatedAngle, 0f, Space.Self);

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