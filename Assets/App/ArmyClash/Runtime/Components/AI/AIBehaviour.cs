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
        [SerializeField] protected AIBehaviourType _aiBehaviourType;
        [SerializeField] protected AITagType _aiTagType;
        [SerializeField] protected AIBehaviour _aiTarget;

        private Transform _transform;
        private Rigidbody _rigidbody;

        public AIBehaviourType AiBehaviourType => _aiBehaviourType;

        public AITagType AITagType { get => _aiTagType; set => _aiTagType = value; }
        public AIBehaviour AITarget { get => _aiTarget; set => _aiTarget = value; }
        public Transform Transform => _transform;
        public Rigidbody Rigidbody => _rigidbody;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}