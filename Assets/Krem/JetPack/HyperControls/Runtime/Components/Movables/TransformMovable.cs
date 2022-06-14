using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Components.Movables
{
    [NodeGraphGroupName("Jet Pack/HyperControls/Movables")]
    [DisallowMultipleComponent]
    public class TransformMovable : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Axis2D _inputAxis;
        [SerializeField, NotNull] protected Transform _transform;

        [Header("Settings")]
        public float sensitivity = 2f;

        public Axis2D InputAxis => _inputAxis;
        public Transform Transform => _transform;
    }
}