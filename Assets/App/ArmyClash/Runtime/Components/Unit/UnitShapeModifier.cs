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
    public class UnitShapeModifier : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected UnitShapeModifierScriptableModel _unitShapeModifierScriptableModel;
        
        [Header("Current Data")]
        [SerializeField] protected UnitShapeModifierModel _unitShapeModifierModel;
        [SerializeField] protected GameObject _currentShapeInstance;

        [Header("Ports")]
        [BindInputSignal(nameof(ResetModel))] public InputSignal CallResetModel;
        public OutputSignal OnModelReset;

        public UnitShapeModifierModel CurrentUnitShapeModifierModel => _unitShapeModifierModel;

        public GameObject CurrentShapeInstance
        {
            get => _currentShapeInstance;
            set
            {
                if (_currentShapeInstance != null)
                {
                    GameObject.Destroy(_currentShapeInstance);
                }

                _currentShapeInstance = value;
            }
        }

        public void ResetModel()
        {
            _unitShapeModifierModel = _unitShapeModifierScriptableModel.Model.Clone<UnitShapeModifierModel>() as UnitShapeModifierModel;
            
            OnModelReset.Invoke();
        }
    }
}