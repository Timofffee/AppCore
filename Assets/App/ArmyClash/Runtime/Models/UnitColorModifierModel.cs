using UnityEngine;

namespace App.ArmyClash.Models
{
    [System.Serializable]
    public class UnitColorModifierModel : UnitModel 
    {
        [Header("Additional Data")]
        [SerializeField] protected Material _material;
    
        public Material Material { get => _material; set => SetField(ref _material, value); }
    }
}