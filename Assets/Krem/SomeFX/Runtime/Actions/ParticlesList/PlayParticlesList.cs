using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace Krem.SomeFX.Actions.ParticlesList
{
    [NodeGraphGroupName("Some FX/ParticlesList")] 
    public class PlayParticlesList : CoreAction
    {
        public InputComponent<Components.ParticlesList> ParticlesList;
        
        protected override bool Action()
        {
            ParticlesList.Component.ParticleSystems.ForEach(particleSystem =>
            {
                particleSystem.Play();
            });
        
            return true;
        }
    }
}