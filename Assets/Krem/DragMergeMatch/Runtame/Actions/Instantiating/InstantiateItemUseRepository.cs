using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class InstantiateItemUseRepository : CoreAction
    {
        public InputComponent<ItemsRepositoryProvider> ItemsRepositoryProvider;
        
        [InjectComponent] private PlaceholdersList _placeholdersList;
        
        protected override bool Action()
        {
            ItemsRepositoryProvider itemsRepositoryProvider = ItemsRepositoryProvider.Component;
            
            if (!_placeholdersList.HasEmptyPlaceholder() || itemsRepositoryProvider.ItemsRepository.Data.Count == 0)
            {
                return false;
            }

            PlaceholderComponent placeholderComponent = _placeholdersList.GetEmptyPlaceholders().First();
            DragMergeItemModel dragMergeItemModel = itemsRepositoryProvider.ItemsRepository.Data.First().Model;
            
            InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(dragMergeItemModel, placeholderComponent);

            return true;
        }
    }
}