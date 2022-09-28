using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CinemachineManagement.Components;
using UnityEngine;

namespace Krem.CinemachineManagement.Actions
{
    [NodeGraphGroupName("Cinemachine Management")] 
    public class FindCinemachineTargetAndLinkToCinemachineVirtualCamera : CoreAction
    {
        public InputComponent<CinemachineVirtualCameraLink> CinemachineVirtualCamera;
        
        protected override bool Action()
        {
            GameObject target = GameObject.FindObjectOfType<CinemachineTarget>().gameObject;

            CinemachineVirtualCamera.Component.VirtualCamera.Follow = target.transform;
            CinemachineVirtualCamera.Component.VirtualCamera.m_LookAt = target.transform;
            
            CinemachineVirtualCamera.Component.VirtualCamera.gameObject.SetActive(true);
        
            return true;
        }
    }
}