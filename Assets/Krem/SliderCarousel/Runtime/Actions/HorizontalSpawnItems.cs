using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Horizontal")] 
    public class HorizontalSpawnItems : CoreAction
    {
        [InjectComponent] private SliderItemsSpawner _sliderItemsSpawner;
        
        protected override bool Action()
        {
            if (_sliderItemsSpawner.ItemPrefabs.Count == 0)
            {
                return false;
            }
            
            
            Vector3 currentPosition = Vector3.zero;
            _sliderItemsSpawner.ItemPrefabs.ForEach(prefab =>
            {
                SliderItem sliderItem = prefab.GetComponent<SliderItem>();
                
                Vector3 actualPosition = currentPosition == Vector3.zero
                    ? currentPosition
                    : currentPosition + sliderItem.size / 2;
                actualPosition.y = 0;
                actualPosition.z = 0;
                
                GameObject instance = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity,
                    _sliderItemsSpawner.ItemsContainer.Transform);

                SliderItem sliderItemInstance = instance.GetComponent<SliderItem>();
                sliderItemInstance.TransformLink.Transform.localPosition = actualPosition;
                sliderItemInstance.TransformLink.Transform.localScale = sliderItemInstance.scale;
                sliderItemInstance.TransformLink.Transform.localRotation = sliderItemInstance.rotation;

                currentPosition = actualPosition + sliderItem.size / 2;
            });
        
            return true;
        }
    }
}