using System;
using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace Krem.SliderCarousel.Components
{
    [NodeGraphGroupName("Slider Carousel")]
    [DisallowMultipleComponent]
    public class SliderItemsSpawner : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField] private TransformLink _itemsContainer;
        
        [Header("Data")]
        [SerializeField] private List<GameObject> _itemPrefabs;

        [Header("Ports")]
        [BindInputSignal(nameof(CallOnSpawnRequest))] public InputSignal SpawnRequest;
        public OutputSignal OnSpawnRequest;

        public TransformLink ItemsContainer => _itemsContainer;
        public List<GameObject> ItemPrefabs => _itemPrefabs;

        private void Awake()
        {
            if (_itemsContainer == null)
            {
                throw new ArgumentNullException(nameof(_itemsContainer));
            }
        }

        public void CallOnSpawnRequest()
        {
            OnSpawnRequest.Invoke();
        }
    }
}