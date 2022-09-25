using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace Krem.DragMergeMatch.Services
{
    public static class InstantiatingItemService
    {
        public static GameObject InstantiateItemFromModelOnPlaceholder(DragMergeItemModel dragMergeItemModel,
            PlaceholderComponent placeholderComponent)
        {
            GameObject itemInstance = GameObject.Instantiate(dragMergeItemModel.ItemPrefab, placeholderComponent.Transform);

            DraggableComponent draggableComponent = itemInstance.GetComponent<DraggableComponent>();
            if (draggableComponent != null)
            {
                draggableComponent.MainCamera = placeholderComponent.MainCamera;
            }

            DragMergeItemModelData instanceDragMergeItemModelData = itemInstance.GetComponent<DragMergeItemModelData>();
            instanceDragMergeItemModelData.dragMergeItemModel = dragMergeItemModel;
            
            itemInstance.transform.position = placeholderComponent.Transform.position;
            itemInstance.transform.localPosition = dragMergeItemModel.placeOffset;
            itemInstance.transform.up = placeholderComponent.Transform.up;

            PlaceableComponent instancePlaceableComponent = itemInstance.GetComponent<PlaceableComponent>();
            placeholderComponent.Attach(instancePlaceableComponent);

            return itemInstance;
        }
    }
}
