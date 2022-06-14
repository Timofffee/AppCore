using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.Events;

namespace Krem.SomeFX.Components
{
    [NodeGraphGroupName("Some FX")]
    public class DissolveMaterial : CoreComponent 
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] private MeshRenderer itemMesh;

        [Header("Ports")]
        public OutputSignal OnRevealRequest;
        public OutputSignal OnHideRequest;

        public MeshRenderer ItemMesh => itemMesh;

        // Interface
        public void RevealRequest()
        {
            OnRevealRequest.Invoke();
        }

        public void HideRequest()
        {
            OnHideRequest.Invoke();
        }
    }
}