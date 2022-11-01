using UnityEngine;

namespace App.ArmyClash.Models
{
    [System.Serializable]
    public class UnitShapeModifierModel : UnitModel 
    {
        [Header("Additional Data")]
        [SerializeField] protected GameObject _shapePrefab;
    
        public GameObject ShapePrefab { get => _shapePrefab; set => SetField(ref _shapePrefab, value); }
    }
}