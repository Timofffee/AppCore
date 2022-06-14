using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.CinemachineManagement.Components;
using UnityEngine;

namespace Krem.CinemachineManagement.Actions
{
    [NodeGraphGroupName("Cinemachine Management")] 
    public class FindCinemachineTargetAndLinkToCinemachineVirtualCamera : CoreAction
    {
        [InjectComponent] private CinemachineVirtualCameraLink _cinemachineVirtualCamera;
        
        protected override bool Action()
        {
            GameObject target = GameObject.FindObjectOfType<CinemachineTarget>().gameObject;

            _cinemachineVirtualCamera.virtualCamera.Follow = target.transform;
            _cinemachineVirtualCamera.virtualCamera.m_LookAt = target.transform;
            
            _cinemachineVirtualCamera.virtualCamera.gameObject.SetActive(true);
        
            return true;
        }
    }
}