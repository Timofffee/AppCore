using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    [DisallowMultipleComponent]
    public class MeshRendererLink : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected MeshRenderer _meshRenderer;

        public MeshRenderer MeshRenderer => _meshRenderer;
    }
}