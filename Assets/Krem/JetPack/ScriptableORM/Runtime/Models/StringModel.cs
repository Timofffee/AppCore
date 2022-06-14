using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Models
{
    [System.Serializable]
    public class StringModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected string _value;
    
        public string Value { get => _value; set => SetField(ref _value, value); }
    }
}