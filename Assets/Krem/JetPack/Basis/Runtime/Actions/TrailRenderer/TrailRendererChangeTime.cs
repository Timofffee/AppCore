using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.TrailRenderer
{
    [NodeGraphGroupName("Jet Pack/Basis/TrailRenderer")]
    public class TrailRendererChangeTime : CoreAction
    {
        [InjectComponent] private UnityEngine.TrailRenderer _trailRenderer;
        
        [ActionParameter] public float time = 1f;
        
        protected override bool Action()
        {
            _trailRenderer.time = time;
            
            return true;
        }
    }
}