using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class TrailRendererLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected TrailRenderer _trailRenderer;

        public TrailRenderer TrailRenderer => _trailRenderer;
    }
}