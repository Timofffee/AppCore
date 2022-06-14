using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class SignalInvoker : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnInvoke;

        public void InvokeSignal()
        {
            OnInvoke.Invoke();
        }
    }
}