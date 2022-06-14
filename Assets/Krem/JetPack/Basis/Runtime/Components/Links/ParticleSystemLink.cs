using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class ParticleSystemLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ParticleSystem _particleSystem;

        [Header("Ports")]
        [BindInputSignal(nameof(Play))]public InputSignal CallPlay;
        public OutputSignal OnPlay;

        public ParticleSystem ParticleSystem => _particleSystem;

        public void Play()
        {
            _particleSystem.Play();
            
            OnPlay.Invoke();
        }
    }
}