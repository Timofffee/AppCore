using System;
using System.Collections.Generic;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.UI;

namespace Krem.JetPack.ProtoElements.Components.RewardFence
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Reward Fence")]
    [DisallowMultipleComponent]
    public class RewardFence : CoreComponent
    {   
        [Header("Components")]
        [SerializeField] private GameObject _itemsHolder;
        [SerializeField] private Image _currentItemIcon;
        [SerializeField] private Image _targetItemIcon;

        [Header("Data")]
        [SerializeField] private int _receivedItemsCount;
        [SerializeField] private List<RewardFenceItem> _items = new List<RewardFenceItem>();

        [Header("Ports")]
        [BindInputSignal(nameof(Refresh))] public InputSignal CallRefresh;
        public OutputSignal OnRefresh;
        public OutputSignal OnReceivedItemsCountSet;

        public int ReceivedItemsCount
        {
            get => _receivedItemsCount;
            set
            {
                if (_items.Count < value)
                {
                    return;
                }

                _receivedItemsCount = value;
                OnReceivedItemsCountSet.Invoke();
            }
        }
        public Image CurrentItemIcon => _currentItemIcon;
        public Image TargetItemIcon => _targetItemIcon;
        public List<RewardFenceItem> Items => _items;

        private void Awake()
        {
            if (_itemsHolder == null)
            {
                throw new ArgumentNullException(nameof(_itemsHolder));
            }
        }

        public void Refresh()
        {
            _items = _itemsHolder.GetComponentsInChildren<RewardFenceItem>().ToList();
            
            OnRefresh.Invoke();
        }
    }
}