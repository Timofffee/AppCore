using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.AudioElements.Components
{
    [NodeGraphGroupName("Jet Pack/Audio Elements")]
    public sealed class Audio : CoreComponent
    {    
        [Header("Dependencies")]
        [NotNull] public AudioSource AudioSource;
        
        [Header("Data")]
        public AudioClip AudioClip;
        public float RandomPitch;

        [Header("Ports")]
        [BindInputSignal(nameof(PlayRequest))] public InputSignal CallPlayRequest;
        public OutputSignal OnPlayRequest;

        public void PlayRequest()
        {
            OnPlayRequest.Invoke();
        }
    }
}