using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions
{
    [NodeGraphGroupName("Drag Merge Match")] 
    public class SetDraggableActiveState : CoreAction 
    {
        [InjectComponent] private DraggableComponent _draggableComponent;

        [ActionParameter] public bool state;
        
        protected override bool Action()
        {
            _draggableComponent.Active = state;
        
            return true;
        }
    }
}