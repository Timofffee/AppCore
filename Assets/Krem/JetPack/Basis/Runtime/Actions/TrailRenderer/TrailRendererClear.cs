using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.TrailRenderer
{
    [NodeGraphGroupName("Jet Pack/Basis/TrailRenderer")]
    public class TrailRendererClear : CoreAction
    {
        [InjectComponent] private UnityEngine.TrailRenderer _trailRenderer;
        
        protected override bool Action()
        {
            _trailRenderer.Clear();
            
            return true;
        }
    }
}