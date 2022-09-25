using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions
{
    [NodeGraphGroupName("Drag Merge Match")] 
    public class SetMergeableActiveState : CoreAction 
    {
        [InjectComponent] private MergeableComponent _mergeableComponent;

        [ActionParameter] public bool state;
        
        protected override bool Action()
        {
            _mergeableComponent.Active = state;
        
            return true;
        }
    }
}