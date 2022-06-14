using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CinemachineManagement.Components;
using UnityEngine;

namespace Krem.CinemachineManagement.Actions
{
    [NodeGraphGroupName("Cinemachine Management")] 
    public class CalculateCinemachineCameraOffset : CoreAction
    {
        public InputData<float> DeltaTime;
        
        [InjectComponent] private CinemachineTargetVelocityOffsetController _cinemachineTargetVelocityOffsetController;

        private Vector3 _value = Vector3.zero;
        
        protected override bool Action()
        {
            _value = _cinemachineTargetVelocityOffsetController.InitialLookAtVector 
                     * _cinemachineTargetVelocityOffsetController.TargetRigidbody.velocity.sqrMagnitude
                     * _cinemachineTargetVelocityOffsetController.distancePerMS
                     + _cinemachineTargetVelocityOffsetController.InitialOffset;
            
            _cinemachineTargetVelocityOffsetController.CinemachineCameraOffset.m_Offset = 
                Vector3.Lerp(
                        _cinemachineTargetVelocityOffsetController.CinemachineCameraOffset.m_Offset,
                        _value,
                        DeltaTime.Data * _cinemachineTargetVelocityOffsetController.offsetChangeSpeed
                    );
            
            return true;
        }
    }
}