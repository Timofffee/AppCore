using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.CoinCollector.Components
{
    [NodeGraphGroupName("CoinCollector")]
    [DisallowMultipleComponent]
    public class CoinCollectible : CoreComponent
    {
        [Header("Data")]
        public int amount;
        
        [Header("Ports")]
        public OutputSignal OnCollisionWithCollector;

        private CoinCollector _coinCollector;

        public CoinCollector CoinCollector => _coinCollector;
        
        private void OnCollisionEnter(Collision other)
        {
            CoinCollector coinCollectorComponent = other.rigidbody
                ? other.rigidbody.gameObject.GetComponent<CoinCollector>()
                : other.gameObject.GetComponent<CoinCollector>();

            if (coinCollectorComponent == null)
            {
                return;
            }

            _coinCollector = coinCollectorComponent;

            OnCollisionWithCollector.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            CoinCollector coinCollectorComponent = other.attachedRigidbody
                ? other.attachedRigidbody.gameObject.GetComponent<CoinCollector>()
                : other.gameObject.GetComponent<CoinCollector>();

            if (coinCollectorComponent == null)
            {
                return;
            }

            _coinCollector = coinCollectorComponent;

            OnCollisionWithCollector.Invoke();
        }
    }
}