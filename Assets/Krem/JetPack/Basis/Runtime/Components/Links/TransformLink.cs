using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class TransformLink : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Transform _transform;
        
        public Transform Transform => _transform;
    }
}