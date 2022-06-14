using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class QuitHandler : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnQuit;

        private void OnApplicationQuit()
        {
            OnQuit.Invoke();
        }
    }
}