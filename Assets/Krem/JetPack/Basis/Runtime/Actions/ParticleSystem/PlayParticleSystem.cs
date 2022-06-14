using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.JetPack.Basis.Actions.ParticleSystem
{
    [NodeGraphGroupName("Jet Pack/Basis/ParticleSystem")] 
    public class PlayParticleSystem : CoreAction
    {
        [InjectComponent] private UnityEngine.ParticleSystem _particleSystem;
        
        protected override bool Action()
        {
            _particleSystem.Play();
        
            return true;
        }
    }
}