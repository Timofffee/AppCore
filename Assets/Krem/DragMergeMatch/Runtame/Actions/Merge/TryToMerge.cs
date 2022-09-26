using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
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
        [InjectComponent] private ItemsRepositoryProvider _itemsRepositoryProvider;

        public OutputSignal OnRevert;

        protected override bool Action()
        {
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(_draggableComponent.PointerEventData, raycastResults);
            
            GameObject raycastedGameObject =
                raycastResults.Find(rr => rr.gameObject.GetComponent<MergeableComponent>() != null 
                                  && rr.gameObject.GetComponent<MergeableComponent>().GetInstanceID() != _mergeableComponent.GetInstanceID()
                                  ).gameObject;

            if (
                raycastedGameObject == null
                || raycastedGameObject.TryGetComponent<MergeableComponent>(out MergeableComponent mergeWithMergeableComponent) == false
            )
            {
                OnRevert.Invoke();
                
                return false;
            }

            if (_mergeableComponent.Active == false || mergeWithMergeableComponent.Active == false)
            {
                OnRevert.Invoke();
                
                return false;
            }

            if (_mergeableComponent.DragMergeItemModelData.dragMergeItemModel.Guid != mergeWithMergeableComponent.DragMergeItemModelData.dragMergeItemModel.Guid)
            {
                OnRevert.Invoke();
                
                return false;
            }
            
            DragMergeScriptableModel nextItem =
                _itemsRepositoryProvider.ItemsRepository.FindNextByGuid(_mergeableComponent.DragMergeItemModelData
                    .dragMergeItemModel.Guid);

            if (nextItem == null)
            {
                OnRevert.Invoke();

                return false;
            }
            
            mergeWithMergeableComponent.MergeRequest(_mergeableComponent);

            return true;
        }
    }
}