using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Components.RewardFence
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Reward Fence")]
    [DisallowMultipleComponent]
    public class RewardFenceLink : CoreComponent
    {    
        [Header("Data")]
        [SerializeField] private RewardFence _rewardFence;

        public RewardFence RewardFence => _rewardFence;
        
        [Header("Ports")]
        [BindInputSignal(nameof(Refresh))] public InputSignal CallRefresh;
        public OutputSignal OnRefresh;

        private void Awake()
        {
            if (_rewardFence == null)
            {
                throw new ArgumentNullException(nameof(_rewardFence));
            }
        }

        public void Refresh()
        {
            _rewardFence.Refresh();
            
            OnRefresh.Invoke();
        }
    }
}