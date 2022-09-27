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
    public class DamageDealer : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitModelProvider _unitModel;
        
        [Header("States")]
        [SerializeField] protected bool _active = true;

        [Header("Ports")]
        [BindInputSignal(nameof(DealDamageRequest))] public InputSignal CallDealDamageRequest;
        public OutputSignal OnDealDamageRequest;
        public OutputSignal OnDealDamage;

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

        public void DealDamageRequest()
        {
            OnDealDamageRequest.Invoke();
        }

        public void DealDamage(DamageReceiver damageReceiver)
        {
            if (Active == false)
            {
                return;
            }
            
            damageReceiver.ApplyDamage(this);
            
            OnDealDamage.Invoke();
        }
    }
}