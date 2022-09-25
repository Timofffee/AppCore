using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class PlaceableComponent : CoreComponent
    {    
        public PlaceholderComponent placeholderComponent;
        
        [SerializeField] private string guid;
        public string Guid => guid;

        private Transform _transform;
        public Transform Transform => _transform;

        public UnityEvent onPlaced;

        private void Awake()
        {
            guid = System.Guid.NewGuid().ToString();
            _transform = transform;
        }
    }
}