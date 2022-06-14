using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.AudioElements.Components;
using UnityEngine;

namespace Krem.JetPack.AudioElements.Actions
{
    [NodeGraphGroupName("Jet Pack/Audio Elements")]
    public class PlayOneShotAudio : CoreAction
    {
        public InputComponent<Audio> AudioComponent;

        protected override bool Action()
        {
            AudioComponent.Component.AudioSource.pitch = 1f + Random.Range(
                -AudioComponent.Component.RandomPitch,
                AudioComponent.Component.RandomPitch
                );
            
            AudioComponent.Component.AudioSource.PlayOneShot(AudioComponent.Component.AudioClip);
            
            return true;
        }
    }
}