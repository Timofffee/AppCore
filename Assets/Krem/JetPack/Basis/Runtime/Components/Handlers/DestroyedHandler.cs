using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class DestroyedHandler : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnDestroyed;

        private void OnDestroy()
        {
            OnDestroyed.Invoke();
        }
    }
}