using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using UnityEngine;

namespace Krem.DragMergeMatch.Actions.Drag
{
    [NodeGraphGroupName("Drag Merge Match/Drag")] 
    public class Drag : CoreAction 
    {
        [InjectComponent] private DraggableComponent _draggable;
        [InjectComponent] private DragMergeItemModelData _dragMergeItemModelData;

        private Vector3 _planeNormal;

        protected override bool Action()
        {
            if (!_draggable.Active)
                return false;

            Vector2 inputRead = _draggable.PointerDragPosition;
            Vector3 mousePosition = new Vector3(inputRead.x, inputRead.y, 0);
            
            Ray currentRay = _draggable.MainCamera.ScreenPointToRay(mousePosition);
            Vector3 currentPosition = _draggable.Transform.position;
            
            // Get move Plane's Normal
            switch (_draggable.dragPlaneOrientation)
            {
                case DragPlaneOrientation.Camera:
                    _planeNormal = _draggable.MainCamera.transform.forward;
                    break;
                
                case DragPlaneOrientation.ObjectForward:
                    _planeNormal = _draggable.Transform.forward;
                    break;
                
                case DragPlaneOrientation.ObjectUp:
                    _planeNormal = _draggable.Transform.up;
                    break;
                
                default:
                    _planeNormal = _draggable.MainCamera.transform.forward;
                    break;
            }

            // plane vs. line intersection in algebric form. It find t as distance from the camera of the new point in the ray's direction.
            float distance = Vector3.Dot(currentPosition - currentRay.origin, _planeNormal) / Vector3.Dot(currentRay.direction, _planeNormal);

            if (_dragMergeItemModelData.dragMergeItemModel.useShadowCloneToDrag)
                _draggable.ShadowClone.transform.position = currentRay.origin + currentRay.direction * distance + _dragMergeItemModelData.dragMergeItemModel.dragOffset;
            else
                _draggable.Transform.position = currentRay.origin + currentRay.direction * distance;

            return true;
        }
    }
}