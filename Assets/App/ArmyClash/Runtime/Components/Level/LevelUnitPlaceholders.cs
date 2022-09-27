using System.Collections.Generic;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Components;
using UnityEngine;

namespace App.ArmyClash.Components.Level
{
    [NodeGraphGroupName("ArmyClash/Level")]
    [DisallowMultipleComponent]
    public class LevelUnitPlaceholders : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField] protected List<PlaceholdersList> _placeholdersLists;

        private List<PlaceableComponent> _placeablesCollection = new List<PlaceableComponent>();

        [Header("Ports")]
        [BindInputSignal(nameof(RefreshPlaceablesCollection))] public InputSignal CallRefreshPlaceablesCollection;
        public OutputSignal OnPlaceableCollectionRefreshed;
        
        public List<PlaceholdersList> PlaceholdersLists => _placeholdersLists;
        public List<PlaceableComponent> PlaceablesCollection => _placeablesCollection;

        public void RefreshPlaceablesCollection()
        {
            _placeablesCollection.Clear();
            
            _placeholdersLists.ForEach(placeholdersList =>
            {
                    placeholdersList.placeholders.FindAll(placeholder => placeholder.AttachedPlaceable != null).ForEach(
                        placeholder =>
                        {
                            _placeablesCollection.Add(placeholder.AttachedPlaceable);
                        });
            });
            
            OnPlaceableCollectionRefreshed.Invoke();
        }
    }
}