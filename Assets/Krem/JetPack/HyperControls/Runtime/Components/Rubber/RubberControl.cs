using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.HyperControls.Scriptables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.JetPack.HyperControls.Components.Rubber
{
    public enum RubberPosition
    {
        Fixed,
        FloatingFixed
    }
    
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")]
    [DisallowMultipleComponent]
    public class RubberControl : Axis2D, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ScriptableAxis2D _scriptableAxis2D;
        
        [Header("Components")]
        [SerializeField, NotNull] protected RectTransform _body;
        [SerializeField, NotNull] protected RectTransform _handle;
        [SerializeField, NotNull] protected RectTransform _center;
        [SerializeField, NotNull] protected RectTransform _trail;

        [Header("Settings")]
        public RubberPosition rubberPosition = RubberPosition.FloatingFixed;
        
        [Header("Output Ports")]
        public OutputSignal PointerDown;
        public OutputSignal PointerDrag;
        public OutputSignal PointerUp;

        private Camera _canvasCamera;
        private Canvas _rootCanvas;
        private RectTransform _rectTransform;
        private PointerEventData _pointerEventData;
        private float _radius;

        public override Vector2 Axis { get => _scriptableAxis2D.Axis; set => _scriptableAxis2D.Axis = value; }
        public RectTransform Body => _body;
        public RectTransform Handle => _handle;
        public RectTransform Center => _center;
        public RectTransform Trail => _trail;
        public Camera CanvasCamera => _canvasCamera;
        public Canvas RootCanvas => _rootCanvas;
        public RectTransform RectTransform => _rectTransform;
        public PointerEventData PointerEventData => _pointerEventData;
        public float Radius => _radius;
        public bool Triggered = false;

        private void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _canvasCamera = RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : RootCanvas.worldCamera;
            _rectTransform = GetComponent<RectTransform>();
            _radius = _body.rect.width / 2;
            
            Trail.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0f);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (Triggered)
            {
                return;
            }
            
            _pointerEventData = eventData;
            
            PointerDown.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (Triggered)
            {
                return;
            }
            
            _pointerEventData = eventData;
            
            PointerDrag.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (Triggered)
            {
                return;
            }
            
            _pointerEventData = eventData;
            
            PointerUp.Invoke();
        }
    }
}