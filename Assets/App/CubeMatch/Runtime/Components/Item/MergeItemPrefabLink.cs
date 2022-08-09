using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace App.CubeMatch.Components.Item
{
    [NodeGraphGroupName("Cube Match/Item")]
    [DisallowMultipleComponent]
    public class MergeItemPrefabLink : CoreComponent
    {
        [SerializeField, NotNull] private GameObject _itemPrefab;

        public GameObject ItemPrefab => _itemPrefab;
    }
}