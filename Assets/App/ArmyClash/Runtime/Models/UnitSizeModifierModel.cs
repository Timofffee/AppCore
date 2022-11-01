using UnityEngine;

namespace App.ArmyClash.Models
{
    [System.Serializable]
    public class UnitSizeModifierModel : UnitModel 
    {
        [Header("Additional Data")]
        [SerializeField] protected float _size = 1f;
    
        public float Size { get => _size; set => SetField(ref _size, value); }
    }
}