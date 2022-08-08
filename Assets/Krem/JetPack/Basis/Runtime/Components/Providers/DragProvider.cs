using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Handlers;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Providers
{
    [NodeGraphGroupName("Jet Pack/Basis/Providers")]
    public class DragProvider : CoreComponent
    {
        [Header("Dependencies")]
        [NotNull] public DragHandler dragHandler;
        
        [Header("Ports")]
        public OutputSignal OnStartDragging;
        public OutputSignal OnDragging;
        public OutputSignal OnEndDragging;

        private void OnEnable()
        {
            dragHandler.OnStartDragging.AddListener(CallOnStartDragging);
            dragHandler.OnDragging.AddListener(CallOnDragging);
            dragHandler.OnEndDragging.AddListener(CallOnEndDragging);
        }

        private void OnDisable()
        {
            dragHandler.OnStartDragging.RemoveListener(CallOnStartDragging);
            dragHandler.OnDragging.RemoveListener(CallOnDragging);
            dragHandler.OnEndDragging.RemoveListener(CallOnEndDragging);
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
    }
}