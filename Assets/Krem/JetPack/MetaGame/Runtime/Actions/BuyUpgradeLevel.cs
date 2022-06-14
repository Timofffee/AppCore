using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.MetaGame.Components;
using Krem.JetPack.ScriptableORM.Components;

namespace Krem.JetPack.MetaGame.Actions
{
    [NodeGraphGroupName("Jet Pack/Meta Game")] 
    public class BuyUpgradeLevel : CoreAction 
    {
        public InputComponent<IntScriptableModelProvider> IntScriptableModelProvider;
        public InputComponent<MetaUpgradeButton> MetaUpgradeButton;
        
        protected override bool Action()
        {
            if (IntScriptableModelProvider.Component.Model.Withdraw(MetaUpgradeButton.Component.UpgradePrice) == false)
            {
                return false;
            }

            MetaUpgradeButton.Component.CurrentLevel.Model.Value++;

            return true;
        }
    }
}