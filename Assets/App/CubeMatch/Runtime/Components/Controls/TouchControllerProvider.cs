using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Components.Controls
{
    [NodeGraphGroupName("Cube Match/Controls")]
    [DisallowMultipleComponent]
    public class TouchControllerProvider : CoreComponent
    {
        [SerializeField, NotNull] private TouchController _touchController;
        
        [Header("Ports")]
        public OutputSignal OnStartDragging;
        public OutputSignal OnDragging;
        public OutputSignal OnEndDragging;
        public OutputSignal OnClick;

        public TouchController TouchController => _touchController;
        
        private void OnEnable()
        {
            _touchController.OnStartDragging.AddListener(CallOnStartDragging);
            _touchController.OnDragging.AddListener(CallOnDragging);
            _touchController.OnEndDragging.AddListener(CallOnEndDragging);
            _touchController.OnClick.AddListener(CallOnClick);
        }

        private void OnDisable()
        {
            _touchController.OnStartDragging.RemoveListener(CallOnStartDragging);
            _touchController.OnDragging.RemoveListener(CallOnDragging);
            _touchController.OnEndDragging.RemoveListener(CallOnEndDragging);
            _touchController.OnClick.RemoveListener(CallOnClick);
        }

        protected void CallOnStartDragging()
        {
            OnStartDragging.Invoke();
        }

        protected void CallOnDragging()
        {
            OnDragging.Invoke();
        }

        protected void CallOnEndDragging()
        {
            OnEndDragging.Invoke();
        }
        
        protected void CallOnClick()
        {
            OnClick.Invoke();
        }
    }
}