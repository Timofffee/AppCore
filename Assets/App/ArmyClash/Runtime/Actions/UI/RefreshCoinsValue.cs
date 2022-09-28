using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.ScriptableORM.Components;

namespace App.ArmyClash.Actions.UI
{
    [NodeGraphGroupName("ArmyClash/UI")] 
    public class RefreshCoinsValue : CoreAction
    {
        public InputComponent<TMPTextLink> TMPTextLink;
        public InputComponent<IntScriptableModelProvider> IntScriptableModelProvider;
        
        protected override bool Action()
        {
            TMPTextLink.Component.TmpText.text = IntScriptableModelProvider.Component.Model.Value.ToString();
        
            return true;
        }
    }
}