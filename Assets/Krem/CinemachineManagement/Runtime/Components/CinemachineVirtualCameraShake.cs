using Cinemachine;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.CinemachineManagement.Components
{
    [NodeGraphGroupName("Cinemachine Management")]
    [DisallowMultipleComponent]
    public class CinemachineVirtualCameraShake : CinemachineVirtualCameraLink
    {
        protected CinemachineBasicMultiChannelPerlin _perlin;

        public CinemachineBasicMultiChannelPerlin Perlin => _perlin;

        private void Awake()
        {
            _perlin = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
}