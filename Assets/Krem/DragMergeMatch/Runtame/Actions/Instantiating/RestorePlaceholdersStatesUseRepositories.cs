using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using Krem.DragMergeMatch.Services;

namespace Krem.DragMergeMatch.Actions.Instantiating
{
    [NodeGraphGroupName("Drag Merge Match/Instantiating")] 
    public class RestorePlaceholdersStatesUseRepositories : CoreAction
    {
        public InputComponent<ItemsRepositoriesProvider> ItemsRepositoriesProvider;
        
        [InjectComponent] private PlaceholdersList _placeholdersList;
        [InjectComponent] private PlaceholdersStatesProvider _placeholdersStatesProvider;

        protected override bool Action()
        {
            _placeholdersList.Clear();
            
            _placeholdersStatesProvider._placeholderStateModelScriptableList.Collection.ForEach(state =>
            {
                PlaceholderComponent placeholder = _placeholdersList.GetPlaceholderByGuid(state.PlaceholderGuid);
                DragMergeItemModel itemModel = null;
                ItemsRepositoriesProvider.Component.ItemsRepositories.ForEach(repository =>
                {
                    DragMergeItemModel foundedItemModel = repository.FindByGuid(state.ItemGuid)?.Model;

                    if (itemModel != null && foundedItemModel != null)
                    {
                        throw new Exception("Duplicate GUIDS in repositoies");
                    }

                    if (foundedItemModel != null)
                    {
                        itemModel = foundedItemModel;
                    }
                });

                InstantiatingItemService.InstantiateItemFromModelOnPlaceholder(itemModel, placeholder);
            });
        
            return true;
        }
    }
}