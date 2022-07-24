using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Interfaces;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Components.Movables
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Movables")]
    [DisallowMultipleComponent]
    public class RigidbodyMovable : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Rigidbody _rigidbody;
        [SerializeField, NotNull] protected Axis2D _inputAxis;

        [Header("Settings")]
        public float sensitivity = 2f;

        public Rigidbody Rigidbody => _rigidbody;
        public IAxis2D InputAxis => _inputAxis;
    }
}