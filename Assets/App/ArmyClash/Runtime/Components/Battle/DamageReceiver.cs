using System.Diagnostics.CodeAnalysis;
using App.ArmyClash.Components.Unit;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Components.Battle
{
    [NodeGraphGroupName("ArmyClash/Battle")]
    [DisallowMultipleComponent]
    public class DamageReceiver : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitModelProvider _unitModel;

        [Header("States")]
        [SerializeField] protected bool _active = true;

        [Header("Ports")]
        public OutputSignal OnDamageApplied;
        public OutputSignal OnDie;
        
        private Transform _transform;

        public UnitModelProvider UnitModel => _unitModel;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        
        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }
        
        public void ApplyDamage(DamageDealer damageDealer)
        {
            if (Active == false)
            {
                return;
            }
            
            UnitModel.CurrentUnitModel.Health -= damageDealer.UnitModel.CurrentUnitModel.AttackDamage;
            OnDamageApplied.Invoke();

            if (UnitModel.CurrentUnitModel.Health <= 0)
            {
                Active = false;
                OnDie.Invoke();
            }
        }
    }
}