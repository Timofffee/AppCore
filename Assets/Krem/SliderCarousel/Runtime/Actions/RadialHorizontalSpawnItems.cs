using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.SliderCarousel.Components;
using UnityEngine;

namespace Krem.SliderCarousel.Actions
{
    [NodeGraphGroupName("Slider Carousel/Radial")] 
    public class RadialHorizontalSpawnItems : CoreAction 
    {
        [InjectComponent] private SliderItemsSpawner _sliderItemsSpawner;
        
        protected override bool Action()
        {
            int itemsCount = _sliderItemsSpawner.ItemPrefabs.Count;
            if (itemsCount == 0)
            {
                return false;
            }
            
            float angleBetweenPieces = 360f / itemsCount;
            
            for (int i = 0; i < itemsCount; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(i * angleBetweenPieces, Vector3.down);
                Vector3 direction = rotation * Vector3.back;

                GameObject itemPrefab = _sliderItemsSpawner.ItemPrefabs[i];
                SliderItem sliderItem = itemPrefab.GetComponent<SliderItem>();

                Vector3 position = _sliderItemsSpawner.ItemsContainer.Transform.position + (direction * sliderItem.size.z);
                GameObject itemInstance = GameObject.Instantiate(itemPrefab, _sliderItemsSpawner.ItemsContainer.Transform);
                itemInstance.transform.position = position;
                
                itemInstance.transform.LookAt(_sliderItemsSpawner.ItemsContainer.Transform, _sliderItemsSpawner.ItemsContainer.Transform.up);
                itemInstance.transform.Rotate(90,0,0, Space.Self);
            }

            return true;
        }
    }
}