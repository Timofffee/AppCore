using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.AI
{
    public enum AIBehaviourType
    {
        Cube,
        Sphere
    }
    
    [NodeGraphGroupName("ArmyClash/AI")]
    [DisallowMultipleComponent]
    public class AIBehaviour : CoreComponent
    {
        [SerializeField] protected AIBehaviourType _aiBehaviourType;

        public AIBehaviourType AiBehaviourType => _aiBehaviourType;
    }
}