using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.AI
{
    public enum AITagType {
        User,
        Bot
    }
    
    [NodeGraphGroupName("ArmyClash/AI")]
    [DisallowMultipleComponent]
    public class AITag : CoreComponent
    {
        [SerializeField] protected AITagType _aiTagType;

        public AITagType AITagType
        {
            get => _aiTagType;
            set => _aiTagType = value;
        }
    }
}