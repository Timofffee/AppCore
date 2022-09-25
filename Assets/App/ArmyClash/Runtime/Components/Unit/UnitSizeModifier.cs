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
    public class UnitSizeModifier : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitSizeModifierScriptableModel _unitSizeModifierScriptableModel;

        [Header("Current Data")]
        [SerializeField] protected UnitSizeModifierModel _unitSizeModifierModel;

        [Header("Ports")]
        [BindInputSignal(nameof(ResetModel))] public InputSignal CallResetModel;
        public OutputSignal OnModelReset;

        public UnitSizeModifierModel CurrentUnitSizeModifierModel => _unitSizeModifierModel;

        public void ResetModel()
        {
            _unitSizeModifierModel = _unitSizeModifierScriptableModel.Model.Clone<UnitSizeModifierModel>() as UnitSizeModifierModel;
            
            OnModelReset.Invoke();
        }
    }
}