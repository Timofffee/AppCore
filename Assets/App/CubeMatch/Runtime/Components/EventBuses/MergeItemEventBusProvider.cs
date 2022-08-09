using App.CubeMatch.EventBuses;
using Krem.AppCore.Attributes;
using Krem.JetPack.EventBus.Components;

namespace App.CubeMatch.Components.EventBuses
{
    [NodeGraphGroupName("Cube Match/Event Buses")]
    public class MergeItemEventBusProvider : BaseEventBusProvider<MergeItemEventBus>
    {
        
    }
}
