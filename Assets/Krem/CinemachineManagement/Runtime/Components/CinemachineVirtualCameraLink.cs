using System;
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
        public CinemachineVirtualCamera virtualCamera;

        private void Awake()
        {
            if (virtualCamera == null)
            {
                throw new ArgumentNullException(nameof(virtualCamera));
            }
        }
    }
}