using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DrawPad.Models;
using Krem.JetPack.ObjectsPool.Components;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Krem.DrawPad.Components
{
    [NodeGraphGroupName("DrawPad")]
    [DisallowMultipleComponent]
    public class DrawPad : BasePool<DrawPadSegment>, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private DrawPadSettingsScriptableModel _settings;
        [SerializeField, NotNull] private Image _backGroundImage;
        
        [Header("States")]
        public bool active = true;

        [Header("Ports")]
        public OutputSignal OnStartDraw;
        public OutputSignal OnDrawing;
        public OutputSignal OnEndDrawing;
        
        // UI components
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private Camera _uiCamera;
        
        // Draw data
        private Vector2 _lastSpawnedPosition;
        private List<DrawPadSegment> _drawnSegments = new List<DrawPadSegment>();
        
        // Pointer Data
        private Vector2 _actualPointerPosition;

        // Properties
        public DrawPadSettingsScriptableModel Settings => _settings;
        public Image BackGroundImage => _backGroundImage;
        public List<DrawPadSegment> DrawnSegments
        {
            get => _drawnSegments;
            set => _drawnSegments = value;
        }

        public int DrawnSegmentsCount => _drawnSegments.Count;
        public int AvailableSegmentsCount => Settings.Model.MaxAvailableSegments - DrawnSegmentsCount;
        public Vector2 ActualPointerPosition => _actualPointerPosition;
        public Vector2 LastSpawnedPosition { get => _lastSpawnedPosition; set => _lastSpawnedPosition = value; }
        public DrawPadSegment CurrentShadowSegment { get; private set; }


        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = gameObject.GetComponentInParent<Canvas>();
            _uiCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
            
            InstantiatePool();
            CurrentShadowSegment = GetFromPool();
            CurrentShadowSegment.gameObject.SetActive(false);

            BackGroundImage.color = Settings.Model.BackgroundColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!active)
                return;
            
            // TouchPad pointer out guard
            if (eventData.pointerCurrentRaycast.gameObject != gameObject)
                return;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.pressPosition, _uiCamera,
                    out _lastSpawnedPosition);
            
            OnStartDraw.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!active)
                return;
            
            // TouchPad pointer out guard
            if (eventData.pointerCurrentRaycast.gameObject != gameObject)
                return;

            // Segments count guard
            if (AvailableSegmentsCount <= 0)
                return;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, _uiCamera,
                out _actualPointerPosition);
            
            OnDrawing.Invoke();
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            if (!active)
                return;

            OnEndDrawing.Invoke();
        }

        public override DrawPadSegment InstantiatePoolObject()
        {
            GameObject segmentInstance = Instantiate(Settings.Model.SegmentPrefab, transform);
            segmentInstance.SetActive(false);

            DrawPadSegment segment = segmentInstance.GetComponent<DrawPadSegment>();
            segment.Pool = this;

            return segment;
        }

        public override DrawPadSegment GetFromPool()
        {
            DrawPadSegment segment =  base.GetFromPool();
            segment.gameObject.SetActive(true);

            return segment;
        }

        public override void ReturnToPool(DrawPadSegment item)
        {
            item.gameObject.SetActive(false);
            base.ReturnToPool(item);
        }
    }
}