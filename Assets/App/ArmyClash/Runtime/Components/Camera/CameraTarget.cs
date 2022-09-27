using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.Camera
{
    [NodeGraphGroupName("ArmyClash/Camera")]
    [DisallowMultipleComponent]
    public class CameraTarget : CoreComponent
    {
        private Transform _transform;

        public event Action<CameraTarget> OnGameObjectDisabled; 

        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnDisable()
        {
            OnGameObjectDisabled?.Invoke(this);
        }
    }
}