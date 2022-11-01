using App.ArmyClash.Components.AI;
using App.ArmyClash.Events;
using Krem.AppCore.Attributes;
using Krem.JetPack.EventBus.Components;
using UnityEngine;

namespace App.ArmyClash.Components.Events
{
    [NodeGraphGroupName("ArmyClash/Events")]
    [DisallowMultipleComponent]
    public class AIBehaviourEventBusProvider : BaseEventBusProvider<AIBehaviourEventBus, AIBehaviour>
    {    
    
    }
}