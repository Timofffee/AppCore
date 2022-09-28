using System.Diagnostics.CodeAnalysis;
using Cinemachine;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.CinemachineManagement.Components
{
    [NodeGraphGroupName("Cinemachine Management")]
    [DisallowMultipleComponent]
    public class CinemachineVirtualCameraLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected CinemachineVirtualCamera _virtualCamera;

        public CinemachineVirtualCamera VirtualCamera => _virtualCamera;
    }
}