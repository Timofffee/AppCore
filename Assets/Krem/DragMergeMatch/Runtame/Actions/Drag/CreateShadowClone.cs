using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class CreateShadowClone : CoreAction 
    {
        [InjectComponent] private DraggableComponent _draggable;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;
        
        protected override bool Action()
        {
            if (_dragMergeItemModelData.dragMergeItemModel.useShadowCloneToDrag == false)
                return false;

            if (_draggable.ShadowClone == null)
            {
                GameObject instanceShadowClone = GameObject.Instantiate(_dragMergeItemModelData.dragMergeItemModel.ItemShadowClone, _draggable.Transform);
                _draggable.ShadowClone = instanceShadowClone.GetComponent<ShadowCloneComponent>();
                _draggable.ShadowClone.original = _draggable;
            }
            else
            {
                _draggable.ShadowClone.gameObject.SetActive(true);
            }

            _draggable.ShadowClone.Transform.position = _draggable.Transform.position + _dragMergeItemModelData.dragMergeItemModel.dragOffset;
        
            return true;
        }
    }
}