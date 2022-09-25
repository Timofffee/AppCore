using System;
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
        public ItemsRepository itemsRepository;

        private void Awake()
        {
            if (itemsRepository == null)
            {
                throw new ArgumentNullException(nameof(itemsRepository));
            }
        }
    }
}