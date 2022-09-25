using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class MergeableComponent : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private DragMergeItemModelData _dragMergeItemModelData; 

        [Header("States")]
        [SerializeField] private bool _active = true;

        [Header("Ports")]
        public OutputSignal OnMergeRequest;

        protected MergeableComponent _mergeWithItem;

        public DragMergeItemModelData DragMergeItemModelData => _dragMergeItemModelData;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public MergeableComponent MergeWith
        {
            get => _mergeWithItem;
            set => _mergeWithItem = value;
        }

        public void MergeRequest(MergeableComponent mergeWithItem)
        {
            _mergeWithItem = mergeWithItem;
            
            OnMergeRequest.Invoke();
        }
    }
}