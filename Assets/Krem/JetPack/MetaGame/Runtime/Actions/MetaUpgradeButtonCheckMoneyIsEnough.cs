using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.MetaGame.Components;
using Krem.JetPack.ScriptableORM.Components;

namespace Krem.JetPack.MetaGame.Actions
{
    [NodeGraphGroupName("Jet Pack/Meta Game")] 
    public class MetaUpgradeButtonCheckMoneyIsEnough : CoreAction
    {
        public InputComponent<IntScriptableModelProvider> IntScriptableModelProvider;

        public OutputSignal OnNotEnough;
        
        [InjectComponent] private MetaUpgradeButton _metaUpgradeButton;
        
        protected override bool Action()
        {
            if (IntScriptableModelProvider.Component.Model.Value < _metaUpgradeButton.UpgradePrice)
            {
                OnNotEnough.Invoke();

                return false;
            }
        
            return true;
        }
    }
}