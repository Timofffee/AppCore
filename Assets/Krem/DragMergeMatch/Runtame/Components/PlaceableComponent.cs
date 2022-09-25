using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

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

        private void Awake()
        {
            guid = System.Guid.NewGuid().ToString();
            _transform = transform;
        }
    }
}