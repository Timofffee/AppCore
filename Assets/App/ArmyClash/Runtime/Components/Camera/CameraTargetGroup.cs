using System.Diagnostics.CodeAnalysis;
using App.ArmyClash.Components.Level;
using Cinemachine;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.Camera
{
    [NodeGraphGroupName("ArmyClash/Camera")]
    [DisallowMultipleComponent]
    public class CameraTargetGroup : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected LevelUnitPlaceholders _levelUnitPlaceholders;
        [SerializeField, NotNull] protected CinemachineTargetGroup _cinemachineTargetGroup;
        
        public LevelUnitPlaceholders LevelUnitPlaceholders => _levelUnitPlaceholders;
        public CinemachineTargetGroup CinemachineTargetGroup => _cinemachineTargetGroup;

        public void RemoveFromGroup(CameraTarget cameraTarget)
        {
            _cinemachineTargetGroup.RemoveMember(cameraTarget.Transform);
        }
    }
}