using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.CoinCollector.Components;
using Krem.JetPack.ScriptableORM.Components;

namespace Krem.CoinCollector.Actions
{
    [NodeGraphGroupName("CoinCollector")] 
    public class CollectCoin : CoreAction
    {
        public InputComponent<IntScriptableModelProvider> IntScriptableModelProvider;

        [InjectComponent] private CoinCollectible _coinCollectible;
        
        protected override bool Action()
        {
            IntScriptableModelProvider.Component.Model.Add(_coinCollectible.amount);
            
            _coinCollectible.CoinCollector.Collect();
        
            return true;
        }
    }
}