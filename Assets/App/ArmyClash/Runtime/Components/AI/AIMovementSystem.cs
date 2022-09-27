using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Components.AI
{
    [NodeGraphGroupName("ArmyClash/AI")]
    [DisallowMultipleComponent]
    public class AIMovementSystem : CoreComponent
    {
        [Header("Settings")]
        [SerializeField] protected float _accelerationSpeed = 1f;

        [Header("States")]
        [SerializeField] protected bool _active;

        [Header("Ports")]
        [BindInputSignal(nameof(EnableSystem))] public InputSignal CallEnableSystem;
        [BindInputSignal(nameof(DisableSystem))] public InputSignal CallDisableSystem;
        public OutputSignal OnSystemEnabled;
        public OutputSignal OnSystemDisabled;

        public float AccelerationSpeed => _accelerationSpeed;
        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value)
                {
                    return;
                }
                
                _active = value;

                if (_active)
                {
                    OnSystemEnabled.Invoke();
                }
                else
                {
                    OnSystemDisabled.Invoke();
                }
            }
        }

        public void EnableSystem()
        {
            Active = true;
        }

        public void DisableSystem()
        {
            Active = false;
        }
    }
}