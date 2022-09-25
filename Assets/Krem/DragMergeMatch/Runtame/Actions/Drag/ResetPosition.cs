using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class ResetPosition : CoreAction 
    {
        [InjectComponent] private PlaceableComponent _placeableComponent;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;
        
        protected override bool Action()
        {
            _placeableComponent.Transform.position = 
                _placeableComponent.placeholderComponent.Transform.position;
            _placeableComponent.Transform.localPosition = _dragMergeItemModelData.dragMergeItemModel.placeOffset;
        
            return true;
        }
    }
}