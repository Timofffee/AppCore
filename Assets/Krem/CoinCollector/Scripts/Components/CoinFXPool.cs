using Krem.AppCore.Attributes;
using Krem.CoinCollector.Scriptables;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace Krem.CoinCollector.Components
{
    [NodeGraphGroupName("CoinCollector")]
    [DisallowMultipleComponent]
    public class CoinFXPool : RectTransformLink
    {    
        [Header("Dependencies")]
        public ScriptableCoinFXPool scriptableFXPool;

        private Camera _uiCamera;

        public Camera UICamera => _uiCamera;

        private void Awake()
        {
            if (scriptableFXPool.instantiated || scriptableFXPool.coinFXPool != null)
            {
                Debug.LogError("Pool Already Instantiated");
                return;
            }
            
            _uiCamera = this.GetComponentInParent<Canvas>().rootCanvas.worldCamera;
            
            scriptableFXPool.PoolItems?.Clear();
            scriptableFXPool.PoolQueue?.Clear();
            scriptableFXPool.coinFXPool = this;
            scriptableFXPool.InstantiatePool();
            scriptableFXPool.instantiated = true;
        }

        private void OnDestroy()
        {
            if (scriptableFXPool.instantiated == false)
            {
                return;
            }
            
            scriptableFXPool.instantiated = false;
            scriptableFXPool.coinFXPool = null;
            scriptableFXPool.PoolItems?.Clear();
            scriptableFXPool.PoolQueue?.Clear();
        }
    }
}