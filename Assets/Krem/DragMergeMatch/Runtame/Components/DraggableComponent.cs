using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.DragMergeMatch.Components
{
    public enum DragPlaneOrientation
    {
        Camera,
        Object
    }
    
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class DraggableComponent : CoreComponent, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {    
        [Header("States")]
        [SerializeField] private bool _active = true;

        [Header("Settings")]
        public DragPlaneOrientation dragPlaneOrientation;
        
        [Header("Ports")]
        public OutputSignal OnStartDrag;
        public OutputSignal OnDragging;
        public OutputSignal OnEndDrag;

        private Transform _transform;
        private Camera _mainCamera;
        
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        public PointerEventData PointerEventData { get; private set; }
        public Vector2 PointerDownPosition { get; private set; }
        public Vector2 PointerDragPosition { get; private set; }
        public Vector2 PointerUpPosition { get; private set; }
        public Transform Transform => _transform;

        public Camera MainCamera
        {
            get => _mainCamera;
            set => _mainCamera = value;
        }
        public ShadowCloneComponent ShadowClone { get; set; }

        private void Awake()
        {
            _transform = transform;
            Canvas canvas = gameObject.GetComponentInParent<Canvas>();
            
            if (canvas == null)
            {
                _mainCamera = Camera.main;
            }
            else
            {
                _mainCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? Camera.main : canvas.worldCamera;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Active)
                return;
            
            PointerEventData = eventData;
            PointerDownPosition = eventData.position;
            
            OnStartDrag.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Active)
                return;
            
            PointerEventData = eventData;
            PointerDragPosition = eventData.position;
            
            OnDragging.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!Active)
                return;
            
            PointerEventData = eventData;
            PointerUpPosition = eventData.position;

            OnEndDrag.Invoke();
        }
    }
}
