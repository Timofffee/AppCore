using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class ShadowCloneComponent : CoreComponent
    {    
        [Header("Data")]
        public DraggableComponent original;

        private Transform _transform;
        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            eventData.hovered.ForEach(item =>
            {
                Debug.Log(item);
            });
        }
    }
}