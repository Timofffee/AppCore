using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Components;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace App.ArmyClash.Components.Placeholders
{
    [NodeGraphGroupName("ArmyClash/Placeholders")]
    [DisallowMultipleComponent]
    public class ArmyGenerator : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected PlaceholderStateModelScriptableList _referencedArmy;
        [SerializeField, NotNull] protected PlaceholdersList _placeholdersList;

        [Header("Settings")]
        [SerializeField] protected float _randomCountPercentage = 30f;

        public PlaceholderStateModelScriptableList ReferencedArmy => _referencedArmy;
        public PlaceholdersList PlaceholdersList => _placeholdersList;
        public float RandomCountPercentage => _randomCountPercentage;
    }
}