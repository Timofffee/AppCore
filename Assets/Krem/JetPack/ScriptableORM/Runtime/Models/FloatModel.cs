using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Models
{
    [System.Serializable]
    public class FloatModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected float _value;
    
        public float Value { get => _value; set => SetField(ref _value, value); }
    }
}