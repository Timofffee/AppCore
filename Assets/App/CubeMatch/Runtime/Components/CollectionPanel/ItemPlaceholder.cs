using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Components.CollectionPanel
{
    [NodeGraphGroupName("Cube Match/CollectionPanel")]
    [DisallowMultipleComponent]
    public class ItemPlaceholder : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnItemSet;
        
        private GameObject _mergeItemInstance;

        public GameObject MergeItemInstance
        {
            get => _mergeItemInstance;
            set
            {
                _mergeItemInstance = value;
                
                OnItemSet.Invoke();
            }
        }
    }
}