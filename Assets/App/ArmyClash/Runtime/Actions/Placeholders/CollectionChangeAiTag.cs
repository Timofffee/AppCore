using App.ArmyClash.Components.AI;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;

namespace App.ArmyClash.Actions.Placeholders
{
    [NodeGraphGroupName("ArmyClash/Placeholders")] 
    public class CollectionChangeAiTag : CoreAction
    {
        public InputComponent<PlaceholdersList> Placeholders;

        [ActionParameter] public AITagType AITagType;
        
        protected override bool Action()
        {
            Placeholders.Component.placeholders.ForEach(placeholder =>
            {
                if (placeholder.AttachedPlaceable == null)
                {
                    return;
                }

                if (placeholder.AttachedPlaceable.gameObject.TryGetComponent(out AIBehaviour aIBehaviour))
                {
                    aIBehaviour.AITagType = AITagType;
                }
            });
        
            return true;
        }
    }
}