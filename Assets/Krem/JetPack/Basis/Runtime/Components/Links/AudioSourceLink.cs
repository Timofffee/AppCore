using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class AudioSourceLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected AudioSource _audioSource;

        public AudioSource AudioSource => _audioSource;
    }
}