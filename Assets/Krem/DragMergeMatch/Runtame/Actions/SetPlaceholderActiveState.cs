using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions
{
    [NodeGraphGroupName("Drag Merge Match")] 
    public class SetPlaceholderActiveState : CoreAction
    {
        [InjectComponent] private PlaceholderComponent _placeholderComponent;

        [ActionParameter] public bool state;
        
        protected override bool Action()
        {
            _placeholderComponent.Active = state;
        
            return true;
        }
    }
}