using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.CubeMatch.Components.FX
{
    [NodeGraphGroupName("Cube Match/FX")]
    [DisallowMultipleComponent]
    public class UITrail : CoreComponent, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField, NotNull] private TrailRenderer _trailRenderer;
        [SerializeField, NotNull] private RectTransform _visualPointer;
        
        private Camera _canvasCamera;
        private Canvas _rootCanvas;
        private RectTransform _rectTransform;

        public TrailRenderer TrailRenderer => _trailRenderer;
        public RectTransform VisualPointer => _visualPointer;
        public Camera CanvasCamera => _canvasCamera;
        public Canvas RootCanvas => _rootCanvas;
        public RectTransform RectTransform => _rectTransform;

        private void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _canvasCamera = RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : RootCanvas.worldCamera;
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SetVisualPointerPosition(eventData.position);
            TrailRenderer.Clear();
            VisualPointer.gameObject.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            SetVisualPointerPosition(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            SetVisualPointerPosition(eventData.position);
            TrailRenderer.Clear();
            VisualPointer.gameObject.SetActive(false);
        }

        protected void SetVisualPointerPosition(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                RectTransform, 
                position, 
                CanvasCamera, 
                out Vector2 touchPosition
            );

            VisualPointer.anchoredPosition = touchPosition;
        }
    }
}