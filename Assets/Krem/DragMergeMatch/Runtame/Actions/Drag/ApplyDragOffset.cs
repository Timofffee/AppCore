using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class ApplyDragOffset : CoreAction 
    {
        [InjectComponent] private DraggableComponent _draggable;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;
        
        protected override bool Action()
        {
            _draggable.Transform.position += _dragMergeItemModelData.dragMergeItemModel.dragOffset;
        
            return true;
        }
    }
}