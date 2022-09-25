using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.DragMergeMatch.Actions.Merge
{
    [NodeGraphGroupName("Drag Merge Match/Merge")]
    public class TryToMerge : CoreAction
    {
        [Header("Data")]
        [InjectComponent] private DraggableComponent _draggableComponent;
        [InjectComponent] private PlaceableComponent _placeableComponent;
        [InjectComponent] private MergeableComponent _mergeableComponent;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;

        public OutputSignal OnRevert;

        protected override bool Action()
        {
            var result = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_draggableComponent.PointerEventData, result);
            
            GameObject raycastedGameObject =
                result.Find(rr => rr.gameObject.GetComponent<MergeableComponent>() != null 
                                  && rr.gameObject.GetComponent<MergeableComponent>().GetInstanceID() != _mergeableComponent.GetInstanceID()
                                  ).gameObject;

            if (
                raycastedGameObject == null
                || raycastedGameObject.TryGetComponent<MergeableComponent>(out MergeableComponent mergeableComponent) == false
            )
            {
                OnRevert.Invoke();
                
                return false;
            }

            if (_mergeableComponent.Active == false || mergeableComponent.Active == false)
            {
                OnRevert.Invoke();
                
                return false;
            }

            if (_mergeableComponent.DragMergeItemModelData.dragMergeItemModel.Guid != mergeableComponent.DragMergeItemModelData.dragMergeItemModel.Guid)
            {
                OnRevert.Invoke();
                
                return false;
            }
            
            mergeableComponent.MergeRequest(_mergeableComponent);

            return true;
        }
    }
}