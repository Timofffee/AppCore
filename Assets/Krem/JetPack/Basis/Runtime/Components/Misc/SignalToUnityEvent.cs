using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.Events;

namespace Krem.JetPack.Basis.Components
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class SignalToUnityEvent : CoreComponent
    {
        [Header("Events")]
        public UnityEvent onInvoke;
        
        [Header("Ports")]
        [BindInputSignal(nameof(InvokeUnityEvent))] public InputSignal CallInvoke;

        private void InvokeUnityEvent()
        {
            onInvoke.Invoke();
        }
    }
}