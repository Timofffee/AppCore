using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Models
{
    [System.Serializable]
    public class BoolModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected bool _value;
    
        public bool Value { get => _value; set => SetField(ref _value, value); }
    }
}