using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Handlers
{
    [NodeGraphGroupName("Jet Pack/Basis/Handlers")]
    public class UpdateHandler : BaseUpdateHandler
    {
        private void Update()
        {
            DeltaTime.Data = Time.deltaTime;
            
            OnUpdate.Invoke();
        }
    }
}