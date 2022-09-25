using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;

namespace Krem.DragMergeMatch.Actions.Data
{
    [NodeGraphGroupName("Drag Merge Match/Data")] 
    public class SerializePlaceholdersStatesToProvider : CoreAction
    {
        [InjectComponent] private PlaceholdersList _placeholdersList;
        [InjectComponent] private PlaceholdersStatesProvider _placeholdersStatesProvider;
        
        protected override bool Action()
        {
            List<PlaceholderStateModel> result = new List<PlaceholderStateModel>();
            
            _placeholdersList.placeholders.ForEach(placeholder =>
            {
                if (placeholder.AttachedPlaceable == null)
                {
                    return;
                }

                PlaceholderStateModel stateModel = new PlaceholderStateModel();
                stateModel.PlaceholderGuid = placeholder.Guid;
                stateModel.ItemGuid = placeholder.AttachedPlaceable.gameObject.GetComponent<DragMergeItemModelData>()
                    .dragMergeItemModel.Guid;
                stateModel.Name = placeholder.gameObject.name;
                
                result.Add(stateModel);
            });

            _placeholdersStatesProvider._placeholderStateModelScriptableList.Collection = result;
        
            return true;
        }
    }
}