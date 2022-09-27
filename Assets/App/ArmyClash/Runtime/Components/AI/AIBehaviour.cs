using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.AI
{
    public enum AIBehaviourType
    {
        Cube,
        Sphere,
        Any
    }
    
    public enum AITagType {
        User,
        Bot,
        Any
    }
    
    [NodeGraphGroupName("ArmyClash/AI")]
    [DisallowMultipleComponent]
    public class AIBehaviour : CoreComponent
    {
        [Header("Data")]
        [SerializeField] protected AIBehaviour _aiTarget;
        
        [Header("Settings")]
        [SerializeField] protected AIBehaviourType _aiBehaviourType;
        [SerializeField] protected AITagType _aiTagType;

        [Header("States")]
        [SerializeField] protected bool _active = true;

        private Transform _transform;
        private Rigidbody _rigidbody;

        public AIBehaviourType AiBehaviourType => _aiBehaviourType;

        public AITagType AITagType { get => _aiTagType; set => _aiTagType = value; }
        public AIBehaviour AITarget { get => _aiTarget; set => _aiTarget = value; }
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        public Transform Transform => _transform;
        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}