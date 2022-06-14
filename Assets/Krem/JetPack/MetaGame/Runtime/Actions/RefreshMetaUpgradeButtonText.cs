using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.MetaGame.Components;

namespace Krem.JetPack.MetaGame.Actions
{
    [NodeGraphGroupName("Jet Pack/Meta Game")] 
    public class RefreshMetaUpgradeButtonText : CoreAction
    {
        [InjectComponent] private MetaUpgradeButton _metaUpgradeButton;
        
        protected override bool Action()
        {
            _metaUpgradeButton.CurrentLevelText.text = _metaUpgradeButton.CurrentLevel.Model.Value.ToString();
            _metaUpgradeButton.UpgradePriceText.text = _metaUpgradeButton.UpgradePrice.ToString();
        
            return true;
        }
    }
}