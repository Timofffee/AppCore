using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class RestorePlaceholdersStates : CoreAction
    {
        [InjectComponent] private PlaceholdersList _placeholdersList;
        [InjectComponent] private ItemsRepositoryProvider _itemsRepositoryProvider;
        [InjectComponent] private PlaceholdersStatesProvider _placeholdersStatesProvider;
        
        protected override bool Action()
        {
            _placeholdersList.Clear();
            
            _placeholdersStatesProvider._placeholderStateModelScriptableList.Collection.ForEach(state =>
            {
                PlaceholderComponent placeholder = _placeholdersList.GetPlaceholderByGuid(state.PlaceholderGuid);
                DragMergeItemModel itemModel =
                    _itemsRepositoryProvider.itemsRepository.FindByGuid(state.ItemGuid).Model;
                
                GameObject itemInstance = InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(itemModel, placeholder);
            });
        
            return true;
        }
    }
}