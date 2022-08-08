using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent]
    public sealed class ExplosionForce : CoreComponent
    {    
        public float force = 10f;
        public float radius = 5f;
        public float upwardsModifier = 1f;
    }
}