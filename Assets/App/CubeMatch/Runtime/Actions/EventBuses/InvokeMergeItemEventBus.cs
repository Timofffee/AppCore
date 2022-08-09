using App.CubeMatch.Components.EventBuses;
using App.CubeMatch.Components.Item;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;

namespace App.CubeMatch.Actions.EventBuses
{
    [NodeGraphGroupName("Cube Match/Event Buses")]
    public class InvokeMergeItemEventBus : CoreAction
    {
        public InputComponent<MergeItemEventBusProvider> MergeEventBusProvider;
        public InputComponent<MergeItem> MergeItem;
        
        protected override bool Action()
        {
            MergeEventBusProvider.Component.EventBus.Invoke(MergeItem.Component);
        
            return true;
        }
    }
}