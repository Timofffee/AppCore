using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using App.ArmyClash.Components.Level;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
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

        [Header("Ports")]
        [BindInputSignal(nameof(Filter))] public InputSignal CallFilter;
        public OutputSignal OnFiltered;
        public OutputSignal OnRemovedAllItems;

        protected List<AIBehaviour> _filteredAIBehaviours = new List<AIBehaviour>();

        public AIBehaviourType BehaviourTypeFilter => _behaviourTypeFilter;
        public AITagType AiTagTypeFilter => _aiTagTypeFilter;
        public LevelUnitPlaceholders LevelUnitPlaceholders => _levelUnitPlaceholders;
        public List<AIBehaviour> FilteredAIBehaviours => _filteredAIBehaviours;

        public void Filter()
        {
            UnsubscribeHandlers();
            
            _filteredAIBehaviours.Clear();
            
            _levelUnitPlaceholders.PlaceablesCollection.ForEach(placeholder =>
            {
                if (placeholder.gameObject.TryGetComponent(out AIBehaviour aiBehaviour))
                {
                    _filteredAIBehaviours.Add(aiBehaviour);
                }
            });

            _filteredAIBehaviours = _filteredAIBehaviours.FindAll(
                behaviour => behaviour.Active 
                             && (_behaviourTypeFilter == AIBehaviourType.Any || behaviour.AiBehaviourType == _behaviourTypeFilter)
                             && (_aiTagTypeFilter == AITagType.Any || behaviour.AITagType == _aiTagTypeFilter)
            );

            SubscribeHandlers();
            
            OnFiltered.Invoke();
        }

        public void AIBehaviourDisableHandler(AIBehaviour aiBehaviour)
        {
            aiBehaviour.OnDisabled -= AIBehaviourDisableHandler;

            _filteredAIBehaviours.Remove(aiBehaviour);

            if (_filteredAIBehaviours.Count == 0)
            {
                OnRemovedAllItems.Invoke();
            }
        }

        protected void SubscribeHandlers()
        {
            _filteredAIBehaviours.ForEach(behaviour =>
            {
                behaviour.OnDisabled += AIBehaviourDisableHandler;
            });
        }
        
        protected void UnsubscribeHandlers()
        {
            _filteredAIBehaviours.ForEach(behaviour =>
            {
                behaviour.OnDisabled -= AIBehaviourDisableHandler;
            });
        }
    }
}