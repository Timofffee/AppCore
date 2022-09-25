using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class PlaceholdersListLink : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField] private PlaceholdersList _placeholdersList;

        public PlaceholdersList PlaceholdersList => _placeholdersList;

        private void Awake()
        {
            if (_placeholdersList == null)
            {
                throw new ArgumentNullException(nameof(_placeholdersList));
            }
        }
    }
}