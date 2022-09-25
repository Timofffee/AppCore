using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Merge
{
    [NodeGraphGroupName("Drag Merge Match/Merge")] 
    public class MergeItems : CoreAction
    {
        [InjectComponent] private MergeableComponent _mergeableComponent;
        [InjectComponent] private ItemsRepositoryProvider _itemsRepositoryProvider;

        public OutputSignal OnRevert;
        
        protected override bool Action()
        {
            if (_mergeableComponent.MergeWith == null 
                || _mergeableComponent.DragMergeItemModelData.dragMergeItemModel.Guid != _mergeableComponent.MergeWith.DragMergeItemModelData.dragMergeItemModel.Guid
                )
            {
                OnRevert.Invoke();
                
                return false;
            }

            DragMergeScriptableModel nextItem =
                _itemsRepositoryProvider.itemsRepository.FindNextByGuid(_mergeableComponent.DragMergeItemModelData
                    .dragMergeItemModel.Guid);

            if (nextItem == null)
            {
                OnRevert.Invoke();

                return false;
            }
            
            MergeableComponent withMergeComponent = _mergeableComponent.MergeWith;
            withMergeComponent.gameObject.GetComponent<PlaceableComponent>()?.placeholderComponent.Detach();
            
            GameObject.Destroy(withMergeComponent.gameObject);
            _mergeableComponent.MergeWith = null;

            _mergeableComponent.DragMergeItemModelData.dragMergeItemModel = nextItem.Model;
        
            return true;
        }
    }
}