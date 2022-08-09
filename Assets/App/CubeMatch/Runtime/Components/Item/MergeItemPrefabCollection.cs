using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.CubeMatch.Components.Item
{
    [NodeGraphGroupName("Cube Match/Item")]
    [DisallowMultipleComponent]
    public class MergeItemPrefabCollection : CoreComponent
    {
        [SerializeField] private List<GameObject> _mergeItemPrefabs;

        public List<GameObject> MergeItemPrefabs => _mergeItemPrefabs;

        public GameObject GetItemByID(int itemId)
        {
            return _mergeItemPrefabs.FindLast(item => item.GetComponent<MergeItem>().ItemId == itemId);
        }
    }
}