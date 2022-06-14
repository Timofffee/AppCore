using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.AudioElements.Actions
{
    [NodeGraphGroupName("Jet Pack/Audio Elements")] 
    public class StopAudioSource : CoreAction
    {
        [InjectComponent] private AudioSource _audioSource;
        
        protected override bool Action()
        {
            _audioSource.Stop();
        
            return true;
        }
    }
}