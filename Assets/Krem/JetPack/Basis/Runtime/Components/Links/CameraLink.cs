using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class CameraLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Camera _camera;

        public Camera Camera => _camera;
    }
}