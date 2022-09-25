using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class PlaceableComponent : CoreComponent
    {   
        [Header("Current Data")]
        [SerializeField] private PlaceholderComponent _placeholderComponent;
        [SerializeField] private string guid;

        [Header("Ports")]
        public OutputSignal OnPlaced;

        private Transform _transform;

        public PlaceholderComponent PlaceholderComponent
        {
            get => _placeholderComponent;
            set
            {
                _placeholderComponent = value;
                
                OnPlaced.Invoke();
            }
        }
        public string Guid => guid;
        public Transform Transform => _transform;

        private void Awake()
        {
            guid = System.Guid.NewGuid().ToString();
            _transform = transform;
        }
    }
}