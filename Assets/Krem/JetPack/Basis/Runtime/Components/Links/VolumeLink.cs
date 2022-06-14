using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;
using UnityEngine.Rendering;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    [DisallowMultipleComponent]
    public class VolumeLink : CoreComponent
    {   
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Volume _volume;

        [Header("States")]
        public bool active = false;

        public Volume Volume => _volume;
    }
}