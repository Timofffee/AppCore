using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ScriptableORM.Components;

namespace App.ArmyClash.Actions.Meta
{
    [NodeGraphGroupName("ArmyClash/Meta")] 
    public class AddReward : CoreAction
    {
        public InputComponent<IntScriptableModelProvider> Reward;
        public InputComponent<IntScriptableModelProvider> UserCoins;
        
        protected override bool Action()
        {
            UserCoins.Component.Model.Add(Reward.Component.Model.Value);
        
            return true;
        }
    }
}