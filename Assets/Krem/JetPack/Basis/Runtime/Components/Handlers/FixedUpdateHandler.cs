using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class FixedUpdateHandler : BaseUpdateHandler
    {
        private void FixedUpdate()
        {
            DeltaTime.Data = Time.fixedDeltaTime;
            
            OnUpdate.Invoke();
        }
    }
}