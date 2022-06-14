using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class LateUpdateHandler : BaseUpdateHandler
    {
        private void LateUpdate()
        {
            DeltaTime.Data = Time.deltaTime;
            
            OnUpdate.Invoke();
        }
    }
}