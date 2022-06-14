using Krem.CoinCollector.Components;
using Krem.JetPack.ObjectsPool.Scriptables;
using UnityEngine;

namespace Krem.CoinCollector.Scriptables
{
    [CreateAssetMenu(fileName = "ScriptableCoinFXPool", menuName = "CoinCollector/ScriptableFXPool")]
    public class ScriptableCoinFXPool : ScriptableBasePool<CoinFX>
    {
        [Header("Dependencies")]
        [SerializeField] private GameObject FXPrefab;
        
        [Header("Runtime Only")]
        public CoinFXPool coinFXPool;
        
        [Header("States")]
        public bool instantiated = false;
        
        public override CoinFX InstantiatePoolObject()
        {
            GameObject FXItemInstance =
                GameObject.Instantiate(FXPrefab, coinFXPool.transform);
            FXItemInstance.SetActive(false);
                
            CoinFX FXItemComponent = FXItemInstance.GetComponent<CoinFX>();

            FXItemComponent.Pool = this;

            return FXItemComponent;
        }
    }
}
