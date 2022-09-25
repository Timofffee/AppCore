using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Scriptables;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class ItemsRepositoryProvider : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected ItemsRepository _itemsRepository;

        public ItemsRepository ItemsRepository => _itemsRepository;
    }
}