using System.Collections.Generic;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace App.CubeMatch.Components.CollectionPanel
{
    [NodeGraphGroupName("Cube Match/CollectionPanel")]
    [DisallowMultipleComponent]
    public class ItemPlaceholderCollector : CoreComponent
    {
        [Header("Ports")]
        [BindInputSignal(nameof(Refresh))] public InputSignal CallRefresh;
        public OutputSignal OnRefresh;
        
        private List<ItemPlaceholder> _itemPlaceholders;

        public List<ItemPlaceholder> ItemPlaceholders => _itemPlaceholders;

        public void Refresh()
        {
            _itemPlaceholders = GetComponentsInChildren<ItemPlaceholder>().ToList();
            
            OnRefresh.Invoke();
        }

        public ItemPlaceholder GetEmptyPlaceholder()
        {
            return _itemPlaceholders.Find(placeholder => placeholder.MergeItemInstance == null);
        }
    }
}