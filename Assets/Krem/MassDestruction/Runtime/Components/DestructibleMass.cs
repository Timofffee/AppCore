using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent]
    public sealed class DestructibleMass : CoreComponent
    {    
        [Header("Data")]
        public float mass = 10f;
    }
}