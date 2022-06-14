using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class RigidbodyLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Rigidbody _rigidbody;

        public Rigidbody Rigidbody => _rigidbody;
    }
}