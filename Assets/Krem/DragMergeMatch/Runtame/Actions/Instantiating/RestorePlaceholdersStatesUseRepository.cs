using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class RestorePlaceholdersStatesUseRepository : CoreAction
    {
        public InputComponent<ItemsRepositoryProvider> ItemsRepositoryProvider;
        
        [InjectComponent] private PlaceholdersList _placeholdersList;
        [InjectComponent] private PlaceholdersStatesProvider _placeholdersStatesProvider;

        protected override bool Action()
        {
            _placeholdersList.Clear();
            
            _placeholdersStatesProvider._placeholderStateModelScriptableList.Collection.ForEach(state =>
            {
                PlaceholderComponent placeholder = _placeholdersList.GetPlaceholderByGuid(state.PlaceholderGuid);
                DragMergeItemModel itemModel =
                    ItemsRepositoryProvider.Component.ItemsRepository.FindByGuid(state.ItemGuid).Model;
                
                InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(itemModel, placeholder);
            });
        
            return true;
        }
    }
}