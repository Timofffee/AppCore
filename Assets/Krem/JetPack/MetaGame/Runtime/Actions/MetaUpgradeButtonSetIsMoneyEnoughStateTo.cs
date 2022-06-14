using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.MetaGame.Components;

namespace Krem.JetPack.MetaGame.Actions
{
    [NodeGraphGroupName("Jet Pack/Meta Game")] 
    public class MetaUpgradeButtonSetIsMoneyEnoughStateTo : CoreAction
    {
        [ActionParameter] public bool State = false;
        
        [InjectComponent] private MetaUpgradeButton _metaUpgradeButton;
        
        protected override bool Action()
        {
            _metaUpgradeButton.IsMoneyEnoughState = State;
        
            return true;
        }
    }
}