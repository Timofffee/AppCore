using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.CubeMatch.Components.FX
{
    [NodeGraphGroupName("Cube Match/FX")]
    [DisallowMultipleComponent]
    public class ClickParticles : CoreComponent, IPointerClickHandler
    {
        [SerializeField, NotNull]private GameObject _clickFXPrefab;
        
        private Camera _canvasCamera;
        private Canvas _rootCanvas;
        private RectTransform _rectTransform;


        public GameObject ClickFXPrefab => _clickFXPrefab;
        public Camera CanvasCamera => _canvasCamera;
        public Canvas RootCanvas => _rootCanvas;
        public RectTransform RectTransform => _rectTransform;

        private GameObject _fxInstance;
        
        private void Awake()
        {
            _rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
            _canvasCamera = RootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : RootCanvas.worldCamera;
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            InstantiateFX(eventData.position);
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
    }
}