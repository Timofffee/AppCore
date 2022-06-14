using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.CoinCollector.Components;

namespace Krem.CoinCollector.Actions
{
    [NodeGraphGroupName("CoinCollector")] 
    public class ReturnCoinFXToPool : CoreAction 
    {
        [InjectComponent] private CoinFX _FXItem;

        protected override bool Action()
        {
            _FXItem.ReturnToPool();

            return true;
        }
    }
}