using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ObjectsPool.Scriptables;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Components
{
    [NodeGraphGroupName("Jet Pack/Objects Pool")]
    public class ScriptableParticlePoolProvider : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ScriptableParticlesPool _scriptableParticlesPool;

        [Header("Ports")]
        [BindInputSignal(nameof(InstantiatePool))] public InputSignal CallInstantiatePool;
        [BindInputSignal(nameof(ClearPool))] public InputSignal CallClearPool;

        public ScriptableParticlesPool ScriptableParticlesPool => _scriptableParticlesPool;

        public void InstantiatePool()
        {
            ScriptableParticlesPool.InstantiatePool();
        }

        public void ClearPool()
        {
            ScriptableParticlesPool.ClearPool();
        }
    }
}