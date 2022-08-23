using System.Diagnostics.CodeAnalysis;
using App.CubeMatch.Components.Item;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.CubeMatch.Components.FX
{
    [NodeGraphGroupName("Cube Match/FX")]
    [DisallowMultipleComponent]
    public class ClickParticles : CoreComponent, IPointerClickHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField, NotNull]private GameObject _clickFXPrefab;
        
        private Camera _canvasCamera;
        private Canvas _rootCanvas;
        private RectTransform _rectTransform;
        private Vector2 _clickPosition = Vector2.zero;


        public GameObject ClickFXPrefab => _clickFXPrefab;
        public Camera CanvasCamera => _canvasCamera;
        public Canvas RootCanvas => _rootCanvas;
        public RectTransform RectTransform => _rectTransform;
        public Vector2 ClickPosition => _clickPosition;

        private GameObject _fxInstance;
        private bool _activeState = true;
        private Camera _mainCamera;
        
        private void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _canvasCamera = RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : RootCanvas.worldCamera;
            _rectTransform = GetComponent<RectTransform>();
            _mainCamera = Camera.main;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_activeState == false)
            {
                return;
            }

            _clickPosition = eventData.position;

            TouchItem(eventData.position);
            InstantiateFX(eventData.position);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _activeState = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _activeState = true;
        }

        protected void InstantiateFX(Vector2 position)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                RectTransform, 
                position, 
                CanvasCamera, 
                out Vector2 touchPosition
            );

            _fxInstance = GameObject.Instantiate(ClickFXPrefab, RectTransform);
            RectTransform rectTransform = _fxInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = touchPosition;
        }

        protected void TouchItem(Vector2 position)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(position);
        
            if (Physics.Raycast(ray, out hit)) {
                GameObject objectHit = hit.transform.gameObject;

                if (objectHit.TryGetComponent(out MergeItem mergeItem))
                {
                    mergeItem.SelectRequest();
                }
            }
        }
    }
}