using App.CubeMatch.Components.Item;
using Krem.JetPack.EventBus;
using UnityEngine;

namespace App.CubeMatch.EventBuses
{
    [CreateAssetMenu(fileName = "MergeItemScriptableEventBus", menuName = "CubeMatch/MergeItem Event Bus")]
    public class MergeItemEventBus : BaseEventBus<MergeItem>
    {

    }
}
