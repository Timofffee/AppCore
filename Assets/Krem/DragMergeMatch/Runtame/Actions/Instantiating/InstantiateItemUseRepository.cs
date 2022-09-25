using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class InstantiateItemUseRepository : CoreAction
    {
        [InjectComponent] private ItemsRepositoryProvider _itemsRepositoryProvider;
        [InjectComponent] private PlaceholdersList _placeholdersList;
        
        protected override bool Action()
        {
            if (!_placeholdersList.HasEmptyPlaceholder() || _itemsRepositoryProvider.ItemsRepository.Data.Count == 0)
            {
                return false;
            }

            PlaceholderComponent placeholderComponent = _placeholdersList.GetEmptyPlaceholders().First();
            DragMergeItemModel dragMergeItemModel = _itemsRepositoryProvider.ItemsRepository.Data.First().Model;
            
            GameObject itemInstance = InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(dragMergeItemModel, placeholderComponent);

            return true;
        }
    }
}