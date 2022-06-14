using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.CoinCollector.Scriptables;
using UnityEngine;

namespace Krem.CoinCollector.Components
{
    [NodeGraphGroupName("CoinCollector")]
    [DisallowMultipleComponent]
    public class CoinFXPoolProvider : CoreComponent
    {    
        [Header("Dependencies")]
        public ScriptableCoinFXPool scriptableFXPool;

        public CoinFX GetFromPool(Vector2 position)
        {
            CoinFX coinFX = scriptableFXPool.GetFromPool();
            
            coinFX.RectTransform.anchoredPosition = position;
            coinFX.gameObject.SetActive(true);
            coinFX.Particles.Play();
            
            coinFX.OnInstantiateFromPool.Invoke();
            
            return coinFX;
        }
    }
}