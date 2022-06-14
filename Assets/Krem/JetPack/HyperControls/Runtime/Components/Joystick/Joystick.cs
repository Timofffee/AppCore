using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.HyperControls.Scriptables;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Krem.JetPack.HyperControls.Components.Joystick
{
    public enum JoystickPosition
    {
        Fixed,
        FloatingFixed,
        Floating
    }
    
    [NodeGraphGroupName("Jet Pack/HyperControls/Joystick")]
    [DisallowMultipleComponent]
    public class Joystick : Axis2D, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ScriptableAxis2D _scriptableAxis2D;
        
        [Header("Components")]
        [SerializeField, NotNull] protected RectTransform _body;
        [SerializeField, NotNull] protected RectTransform _handle;

        [Header("Settings")]
        public JoystickPosition joystickPosition = JoystickPosition.FloatingFixed;

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
        public Camera CanvasCamera => _canvasCamera;
        public Canvas RootCanvas => _rootCanvas;
        public RectTransform RectTransform => _rectTransform;
        public PointerEventData PointerEventData => _pointerEventData;
        public float Radius => _radius;

        private void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _canvasCamera = RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : RootCanvas.worldCamera;
            _rectTransform = GetComponent<RectTransform>();
            _radius = _body.rect.width / 2;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            
            PointerDown.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            
            PointerDrag.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointerEventData = eventData;
            
            PointerUp.Invoke();
        }
    }
}