using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class IsShadowCloneUsed : CoreAction 
    {
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;

        public OutputSignal isNotUsed;
        
        protected override bool Action()
        {
            if (_dragMergeItemModelData.dragMergeItemModel.useShadowCloneToDrag == false)
            {
                isNotUsed.Invoke();
                
                return false;
            }
            
            return true;
        }
    }
}