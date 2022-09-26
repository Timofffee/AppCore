using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Extensions;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Scriptables;
using Krem.DragMergeMatch.Services;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class InstantiateRandomItemUseRandomRepository : CoreAction
    {
        public InputComponent<ItemsRepositoriesProvider> ItemsRepositoriesProvider;
        
        [InjectComponent] private PlaceholdersList _placeholdersList;
        
        protected override bool Action()
        {
            ItemsRepositoriesProvider itemsRepositoriesProvider = ItemsRepositoriesProvider.Component;
            
            if (!_placeholdersList.HasEmptyPlaceholder() || itemsRepositoriesProvider.ItemsRepositories.Count == 0)
            {
                return false;
            }

            ItemsRepository itemsRepository = itemsRepositoriesProvider.ItemsRepositories.Random();

            if (itemsRepository.Data.Count == 0)
            {
                return false;
            }

            PlaceholderComponent placeholderComponent = _placeholdersList.GetEmptyPlaceholders().First();
            DragMergeItemModel dragMergeItemModel = itemsRepository.Data.Random().Model;
            
            InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(dragMergeItemModel, placeholderComponent);

            return true;
        }
    }
}