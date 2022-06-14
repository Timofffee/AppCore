using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.SomeFX.Components
{
    [NodeGraphGroupName("Some FX")]
    public class ParticlesList : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField] protected List<ParticleSystem> _particleSystems;

        public List<ParticleSystem> ParticleSystems => _particleSystems;
    }
}