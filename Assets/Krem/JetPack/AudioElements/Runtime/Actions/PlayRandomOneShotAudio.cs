using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.AudioElements.Components;
using UnityEngine;

namespace Krem.JetPack.AudioElements.Actions
{
    [NodeGraphGroupName("Jet Pack/Audio Elements")]
    public class PlayRandomOneShotAudio : CoreAction
    {
        public InputComponent<AudioList> AudioListComponent;

        protected override bool Action()
        {
            AudioListComponent.Component.AudioSource.pitch = 1f + Random.Range(
                -AudioListComponent.Component.RandomPitch,
                AudioListComponent.Component.RandomPitch
                );
            
            AudioListComponent.Component.AudioSource.PlayOneShot(AudioListComponent.Component.GetRandomAudioClip());
            
            return true;
        }
    }
}