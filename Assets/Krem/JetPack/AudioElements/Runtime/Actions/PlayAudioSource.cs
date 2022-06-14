using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.AudioElements.Actions
{
    [NodeGraphGroupName("Jet Pack/Audio Elements")] 
    public class PlayAudioSource : CoreAction
    {
        [InjectComponent] private AudioSource _audioSource;
        
        protected override bool Action()
        {
            _audioSource.Play();
        
            return true;
        }
    }
}