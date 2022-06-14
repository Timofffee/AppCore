using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Models
{
    [System.Serializable]
    public class IntModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected int _value;
    
        public int Value { get => _value; set => SetField(ref _value, value); }

        public void Add(int amount)
        {
            Value += amount;
        }

        public bool Withdraw(int amount)
        {
            if (amount > Value)
            {
                return false;
            }

            Value -= amount;

            return true;
        }
    }
}