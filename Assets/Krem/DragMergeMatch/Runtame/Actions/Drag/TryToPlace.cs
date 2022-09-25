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
    public class TryToPlace : CoreAction 
    {
        [Header("Data")]
        [InjectComponent] private DraggableComponent _draggableComponent;
        [InjectComponent] private PlaceableComponent _placeableComponent;
        
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
                OnRevert.Invoke();
                
                return false;
            }

            // Check Placeholder is Active
            if (placeholderComponent.Active == false)
            {
                OnRevert.Invoke();
                
                return false; 
            }
            
            // Check placeholder is Empty
            if (placeholderComponent.IsEmpty() == false
                && _placeableComponent.Guid != placeholderComponent.AttachedPlaceable.Guid
            )
            {
                placeholderComponent.Overflow(_placeableComponent);
                
                OnRevert.Invoke();

                return false;
            }
            
            _placeableComponent.placeholderComponent.Detach();
            placeholderComponent.Attach(_placeableComponent);
            
            return true;
        }
    }
}