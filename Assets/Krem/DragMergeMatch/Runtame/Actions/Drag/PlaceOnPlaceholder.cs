using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class PlaceOnPlaceholder : CoreAction 
    {
        [Header("Data")]
        [InjectComponent] private DraggableComponent _draggableComponent;
        [InjectComponent] private PlaceableComponent _placeableComponent;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;
        
        [Header("Ports")]
        public OutputSignal OnRevert;
        
        protected override bool Action()
        {
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_draggableComponent.PointerEventData, result);
            GameObject raycastedGameObject =
                result.Find(rr => rr.gameObject.GetComponent<PlaceholderComponent>() != null).gameObject;

            // Check raycast
            if (
                raycastedGameObject == null 
                || raycastedGameObject.TryGetComponent(out PlaceholderComponent placeholderComponent) == false
                )
            {
                RevertOnPlaceholder();
                OnRevert.Invoke();
                
                return false;
            }
            
            // Check Placeholder is Active
            if (placeholderComponent.Active == false)
            {
                RevertOnPlaceholder();
                OnRevert.Invoke();
                
                return false; 
            }

            // Check placeholder is Empty
            if (placeholderComponent.IsEmpty() == false
                && _placeableComponent.Guid != placeholderComponent.AttachedPlaceable.Guid
            )
            {
                RevertOnPlaceholder();
                OnRevert.Invoke();
                
                placeholderComponent.Overflow(_placeableComponent);
                
                return false;
            }
            
            PlaceObjectOnPlaceholder(placeholderComponent, _placeableComponent);
            
            return true;
        }

        private void PlaceObjectOnPlaceholder(PlaceholderComponent newPlaceholder, PlaceableComponent placeableComponent)
        {
            placeableComponent.placeholderComponent.Detach();
            newPlaceholder.Attach(_placeableComponent);
            placeableComponent.Transform.position = newPlaceholder.Transform.position;
            placeableComponent.Transform.localPosition = _dragMergeItemModelData.dragMergeItemModel.placeOffset;
        }

        private void RevertOnPlaceholder()
        {
            _placeableComponent.Transform.position = 
                _placeableComponent.placeholderComponent.Transform.position;
            _placeableComponent.Transform.localPosition = _dragMergeItemModelData.dragMergeItemModel.placeOffset;
        }
        
    }
}
