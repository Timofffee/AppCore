using System;
using System.Collections.Generic;
using System.Linq;
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
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_draggableComponent.PointerEventData, raycastResults);
            PlaceholderComponent placeholderComponent = null;

            try
            {
                raycastResults.First(rr => rr.gameObject.TryGetComponent<PlaceholderComponent>(out placeholderComponent));
            }
            catch (Exception e)
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
                placeholderComponent.Overflow(_placeableComponent);
                
                OnRevert.Invoke();

                return false;
            }
            
            PlaceObjectOnPlaceholder(placeholderComponent, _placeableComponent);
            
            return true;
        }

        private void PlaceObjectOnPlaceholder(PlaceholderComponent newPlaceholder, PlaceableComponent placeableComponent)
        {
            placeableComponent.PlaceholderComponent.Detach();
            newPlaceholder.Attach(_placeableComponent);
            placeableComponent.Transform.position = newPlaceholder.Transform.position;
            placeableComponent.Transform.localPosition = _dragMergeItemModelData.dragMergeItemModel.placeOffset;
        }

        private void RevertOnPlaceholder()
        {
            _placeableComponent.Transform.position = 
                _placeableComponent.PlaceholderComponent.Transform.position;
            _placeableComponent.Transform.localPosition = _dragMergeItemModelData.dragMergeItemModel.placeOffset;
        }
        
    }
}
