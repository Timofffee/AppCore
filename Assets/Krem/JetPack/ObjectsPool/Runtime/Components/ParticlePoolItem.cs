using System;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.ObjectsPool.Interfaces;
using Krem.JetPack.ObjectsPool.Scriptables;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Components
{
    [NodeGraphGroupName("Jet Pack/Objects Pool")]
    [DisallowMultipleComponent]
    public class ParticlePoolItem : ParticleSystemLink, IPoolItem
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ScriptableParticlesPool _pool;

        [Header("Ports")]
        [BindInputSignal(nameof(ReturnToPool))] public InputSignal CallReturnToPool;

        private Transform _transform;
        
        public ScriptableParticlesPool Pool
        {
            get => _pool;
            set => _pool = value;
        }

        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
            Pool.ReturnToPool(this);
        }
    }
}