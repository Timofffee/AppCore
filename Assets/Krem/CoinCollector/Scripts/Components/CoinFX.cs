using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CoinCollector.Scriptables;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.ObjectsPool.Interfaces;
using UnityEngine;

namespace Krem.CoinCollector.Components
{
    [NodeGraphGroupName("CoinCollector")]
    [DisallowMultipleComponent]
    public class CoinFX : RectTransformLink, IPoolItem
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ParticleSystem _particleSystem;

        [Header("Ports")]
        public OutputSignal OnInstantiateFromPool;

        public ParticleSystem Particles => _particleSystem;
        public ScriptableCoinFXPool Pool { get; set; }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            Pool.ReturnToPool(this);
        }
    }
}