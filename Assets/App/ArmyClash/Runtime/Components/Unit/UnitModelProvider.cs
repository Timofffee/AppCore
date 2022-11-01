using System.Diagnostics.CodeAnalysis;
using App.ArmyClash.Models;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.ArmyClash.Components.Unit
{
    [NodeGraphGroupName("ArmyClash/Unit")]
    [DisallowMultipleComponent]
    public class UnitModelProvider : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitScriptableModel _unitScriptableModel;

        [Header("Current Data")]
        [SerializeField] protected UnitModel _unitModel;

        [Header("Ports")]
        [BindInputSignal(nameof(ResetModel))] public InputSignal CallResetModel;
        public OutputSignal OnModelReset;

        public UnitModel CurrentUnitModel => _unitModel;

        public void ResetModel()
        {
            _unitModel = _unitScriptableModel.Model.Clone<UnitModel>() as UnitModel;
            
            OnModelReset.Invoke();
        }

        public void ApplyModelModifier(UnitModel model)
        {
            _unitModel.Health += model.Health;
            _unitModel.AttackDamage += model.AttackDamage;
            _unitModel.AttackRadius += model.AttackRadius;
            _unitModel.AttackSpeed += model.AttackSpeed;
            _unitModel.MoveSpeed += model.MoveSpeed;
        }
    }
}