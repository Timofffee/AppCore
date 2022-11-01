using App.ArmyClash.Components.AI;
using Krem.JetPack.EventBus;
using UnityEngine;

namespace App.ArmyClash.Events
{
    [CreateAssetMenu(fileName = "AIBehaviourScriptableEventBus", menuName = "ArmyClash/EventBus/AIBehaviour")]
    public class AIBehaviourEventBus : BaseEventBus<AIBehaviour>
    {

    }
}
