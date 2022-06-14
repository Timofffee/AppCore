using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class ColliderLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Collider _collider;

        public Collider Collider => _collider;
    }
}