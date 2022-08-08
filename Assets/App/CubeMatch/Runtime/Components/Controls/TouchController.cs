using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Handlers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.CubeMatch.Components.Controls
{
    [NodeGraphGroupName("Cube Match/Controls")]
    [DisallowMultipleComponent]
    public class TouchController : DragHandler, IPointerClickHandler
    {
        public OutputSignal OnClick;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            
            //Debug.Log("BeginDrag");
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            //Debug.Log("Drag");
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            
            //Debug.Log("End Drag");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsDragging)
            {
                return;
            }
            
            //Debug.Log("Click");
            
            OnClick.Invoke();
        }
    }
}