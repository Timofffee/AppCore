using App.ArmyClash.Components.Camera;
using Cinemachine;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.ArmyClash.Actions.Camera
{
    [NodeGraphGroupName("ArmyClash/Camera")] 
    public class AddUnitsToCameraGroup : CoreAction
    {
        public InputComponent<CameraTargetGroup> CameraTargetGroup;
        
        protected override bool Action()
        {
            CameraTargetGroup.Component.CinemachineTargetGroup.m_Targets = new CinemachineTargetGroup.Target[0];
            
            CameraTargetGroup.Component.LevelUnitPlaceholders.PlaceablesCollection.ForEach(placeable =>
            {
                CameraTarget cameraTarget = placeable.Transform.GetComponent<CameraTarget>();
                if (cameraTarget == null)
                {
                    return;
                }
                
                CameraTargetGroup.Component.CinemachineTargetGroup.AddMember(cameraTarget.Transform, 1, 0);
                cameraTarget.OnGameObjectDisabled += CameraTargetGroup.Component.RemoveFromGroup;
            });
        
            return true;
        }
    }
}