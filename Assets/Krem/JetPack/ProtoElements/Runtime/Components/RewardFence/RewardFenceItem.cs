using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Components.RewardFence
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Reward Fence")]
    [DisallowMultipleComponent]
    public class RewardFenceItem : CoreComponent
    {    
        [Header("Components")]
        [SerializeField] private GameObject _receivedStateMarker;

        [Header("States")]
        [SerializeField] private bool _receivedState;

        [Header("Ports")]
        public OutputSignal OnStateChanged;

        public GameObject ReceivedStateMarker => _receivedStateMarker;
        public bool ReceivedState
        {
            get => _receivedState;
            set
            {
                if (_receivedState == value)
                {
                    return;
                }

                _receivedState = value;
                _receivedStateMarker.SetActive(value);
                
                OnStateChanged.Invoke();
            }
        }

        private void Awake()
        {
            if (_receivedStateMarker == null)
            {
                throw new ArgumentNullException(nameof(_receivedStateMarker));
            }
            
            _receivedStateMarker.SetActive(_receivedState);
        }
    }
}