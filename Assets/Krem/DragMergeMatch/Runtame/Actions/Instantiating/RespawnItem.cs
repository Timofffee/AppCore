using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class RespawnItem : CoreAction 
    {
        [Header("Data")]
        [InjectComponent] private MergeableComponent _mergeableComponent;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;
        [InjectComponent] private Transform _transform;

        
        protected override bool Action()
        {
            DragMergeItemModel currentItemModel = _dragMergeItemModelData.dragMergeItemModel;
            PlaceableComponent currentPlaceableComponent = _mergeableComponent.gameObject.GetComponent<PlaceableComponent>();
            
            // GameObject itemInstance = GameObject.Instantiate(currentItemModel.ItemPrefab);
            // itemInstance.transform.parent = _transform.parent.transform;
            
            GameObject itemInstance = InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(currentItemModel, currentPlaceableComponent.PlaceholderComponent);
            GameObject.Destroy(_transform.gameObject);

            return true;
        }
    }
}