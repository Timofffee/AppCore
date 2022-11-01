using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ScriptableORM.Components;

namespace App.ArmyClash.Actions.Meta
{
    [NodeGraphGroupName("ArmyClash/Meta")] 
    public class TryToBuyUnit : CoreAction
    {
        public InputComponent<IntScriptableModelProvider> BuyPrice;
        public InputComponent<IntScriptableModelProvider> UserCoins;

        public OutputSignal OnWithdrawFailed;
        
        protected override bool Action()
        {
            if (UserCoins.Component.Model.Withdraw(BuyPrice.Component.Model.Value) == false)
            {
                OnWithdrawFailed.Invoke();

                return false;
            }

            return true;
        }
    }
}