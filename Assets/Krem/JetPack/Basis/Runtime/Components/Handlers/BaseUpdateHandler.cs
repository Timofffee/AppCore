using Krem.AppCore;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    public abstract class BaseUpdateHandler : CoreComponent
    {
        [Header("Ports")]
        public OutputData<float> DeltaTime;
        public OutputSignal OnUpdate;
    }
}