using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class DragMergeItemModelData : CoreComponent
    {    
        [Header("Data")]
        public DragMergeItemModel dragMergeItemModel;
    }
}