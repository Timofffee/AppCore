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
    public class UnitColorModifier : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitColorModifierScriptableModel _unitColorModifierScriptableModel;

        [Header("Current Data")]
        [SerializeField] protected UnitColorModifierModel _unitColorModifierModel;

        [Header("Ports")]
        [BindInputSignal(nameof(ResetModel))] public InputSignal CallResetModel;
        public OutputSignal OnModelReset;

        public UnitColorModifierModel CurrentUnitColorModifierModel => _unitColorModifierModel;

        public void ResetModel()
        {
            _unitColorModifierModel = _unitColorModifierScriptableModel.Model.Clone<UnitColorModifierModel>() as UnitColorModifierModel;
            
            OnModelReset.Invoke();
        }
    }
}