using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")]
    public class DestroyShadowClone : CoreAction
    {
        [Header("Data")]
        [InjectComponent] private DraggableComponent _draggableComponent;

        protected override bool Action()
        {
            if (_draggableComponent.ShadowClone == null)
            {
                return false;
            }
            
            _draggableComponent.ShadowClone.gameObject.SetActive(false);

            return true;
        }
    }
}