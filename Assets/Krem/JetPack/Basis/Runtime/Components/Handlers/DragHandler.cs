using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class DragHandler : CoreComponent, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Header("Ports")]
        public OutputSignal OnStartDragging;
        public OutputSignal OnDragging;
        public OutputSignal OnEndDragging;
        
        private PointerEventData _pointerEventData;
        private Vector2 _startPosition;
        private Vector2 _endPosition;
        
        public PointerEventData PointerEventData => _pointerEventData;
        public Vector2 StartPosition => _startPosition;
        public Vector2 EndPosition => _endPosition;
        public bool IsDragging { get; set; } = false;

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            IsDragging = true;
            _pointerEventData = eventData;
            _startPosition = eventData.position;
            
            OnStartDragging.Invoke();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            
            OnDragging.Invoke();
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            _pointerEventData = eventData;
            _endPosition = eventData.position;
            
            OnEndDragging.Invoke();
        }
    }
}