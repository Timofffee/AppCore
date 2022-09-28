using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CinemachineManagement.Components;

namespace Krem.CinemachineManagement.Actions
{
    [NodeGraphGroupName("Cinemachine Management")] 
    public class CameraShakeSetValueTo : CoreAction
    {
        [ActionParameter] public float ShakeValue;
        
        public InputComponent<CinemachineVirtualCameraShake> CinemachineVirtualCameraShake;
        
        protected override bool Action()
        {
            CinemachineVirtualCameraShake.Component.Perlin.m_AmplitudeGain = ShakeValue;
        
            return true;
        }
    }
}