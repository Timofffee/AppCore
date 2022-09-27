using System.Diagnostics.CodeAnalysis;
using App.ArmyClash.Components.Level;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.ArmyClash.Components.AI
{
    [NodeGraphGroupName("ArmyClash/AI")]
    public class AIFilteredCollection : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected LevelUnitPlaceholders _levelUnitPlaceholders;
        
        [Header("Settings")]
        [SerializeField] protected AIBehaviourType _behaviourTypeFilter;
        [SerializeField] protected AITagType _aiTagTypeFilter;
    }
}