using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Scriptables;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class ItemsRepositoriesProvider : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField] protected List<ItemsRepository> _itemsRepositories;

        public List<ItemsRepository> ItemsRepositories => _itemsRepositories;
    }
}