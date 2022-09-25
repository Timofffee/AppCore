using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace App.ArmyClash.Models
{
    [System.Serializable]
    public class UnitModel : Model 
    {
        [Header("Data")]
        [SerializeField] protected float _health;
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected float _attackDamage;
        [SerializeField] protected float _attackSpeed;
        [SerializeField] protected float _attackRadius;
    
        public float Health { get => _health; set => SetField(ref _health, value); }
        public float MoveSpeed { get => _moveSpeed; set => SetField(ref _moveSpeed, value); }
        public float AttackDamage { get => _attackDamage; set => SetField(ref _attackDamage, value); }
        public float AttackSpeed { get => _attackSpeed; set => SetField(ref _attackSpeed, value); }
        public float AttackRadius { get => _attackRadius; set => SetField(ref _attackRadius, value); }
    }
}