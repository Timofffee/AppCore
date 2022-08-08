using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent]
    public sealed class FracturedPart : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnDestroyRequest;

        public void DestroyRequest()
        {
            OnDestroyRequest.Invoke();
        }
    }
}