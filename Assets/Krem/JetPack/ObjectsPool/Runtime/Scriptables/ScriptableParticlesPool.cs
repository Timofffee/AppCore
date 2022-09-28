using System.Diagnostics.CodeAnalysis;
using Krem.JetPack.ObjectsPool.Components;
using UnityEngine;

namespace Krem.JetPack.ObjectsPool.Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableParticlesPool", menuName = "Jet Pack/Objects Pool/ScriptableParticlesPool", order = 0)]
    public class ScriptableParticlesPool : ScriptableBasePool<ParticlePoolItem>
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected GameObject _particlePrefab;

        public GameObject ParticlePrefab => _particlePrefab;
        
        public override ParticlePoolItem InstantiatePoolObject()
        {
            GameObject particlePoolInstance = GameObject.Instantiate(ParticlePrefab);
            ParticlePoolItem particlePoolItem = particlePoolInstance.GetComponent<ParticlePoolItem>();
            particlePoolItem.Pool = this;
            particlePoolItem.gameObject.SetActive(false);

            return particlePoolItem;
        }

        public override void ClearPool()
        {
            PoolItems.ForEach(item =>
            {
                if (item == null || item.gameObject == null)
                {
                    return;
                }
                
                Destroy(item.gameObject);
            });
            
            PoolItems.Clear();
            PoolQueue.Clear();

            _instantiated = false;
        }
    }
}
