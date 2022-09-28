using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ObjectsPool.Components;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Actions
{
    [NodeGraphGroupName("Jet Pack/Objects Pool")] 
    public class InstantiateItemFromScriptableParticlePool : CoreAction
    {
        public InputComponent<ScriptableParticlePoolProvider> ScriptableParticlePoolProvider;
        [InjectComponent] private Transform _transform;
        
        protected override bool Action()
        {
            ParticlePoolItem particlePoolItem = ScriptableParticlePoolProvider.Component.ScriptableParticlesPool.GetFromPool();
            particlePoolItem.Transform.position = _transform.position;
            particlePoolItem.Transform.rotation = _transform.rotation;
            particlePoolItem.gameObject.SetActive(true);
            particlePoolItem.Play();
        
            return true;
        }
    }
}