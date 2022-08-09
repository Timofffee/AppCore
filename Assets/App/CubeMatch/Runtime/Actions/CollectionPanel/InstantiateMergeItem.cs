using App.CubeMatch.Components.CollectionPanel;
using App.CubeMatch.Components.EventBuses;
using App.CubeMatch.Components.Item;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Actions.CollectionPanel
{
    [NodeGraphGroupName("Cube Match/CollectionPanel")] 
    public class InstantiateMergeItem : CoreAction
    {
        public InputComponent<MergeItemPrefabCollection> MergeItemPrefabCollection;
        public InputComponent<MergeItemEventBusProvider> MergeItemEventBusProvider;
        public InputComponent<ItemPlaceholderCollector> ItemPlaceholderCollector;

        public OutputSignal OnOverflow;

        protected override bool Action()
        {
            ItemPlaceholder placeholder = ItemPlaceholderCollector.Component.GetEmptyPlaceholder();

            if (placeholder == null)
            {
                OnOverflow.Invoke();

                return false;
            }
            
            GameObject itemPrefab =
                MergeItemPrefabCollection.Component.GetItemByID(MergeItemEventBusProvider.Component.EventBus.value
                    .ItemId);

            GameObject itemInstance = GameObject.Instantiate(itemPrefab, placeholder.transform);
            placeholder.MergeItemInstance = itemInstance;

            itemInstance.transform.localScale = new Vector3(100, 100, 100);
            
            int layerMask = LayerMask.NameToLayer("UI");
            itemInstance.layer = layerMask;
            foreach (Transform componentsInChild in itemInstance.GetComponentsInChildren<Transform>())
            {
                componentsInChild.gameObject.layer = layerMask;
            }

            return true;
        }
    }
}