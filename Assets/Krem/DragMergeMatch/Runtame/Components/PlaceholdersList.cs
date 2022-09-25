using System.Linq;
using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public sealed class PlaceholdersList : CoreComponent
    {
        [Header("Data")]
        public List<PlaceholderComponent> placeholders = new List<PlaceholderComponent>();

        [Header("Ports")]
        [BindInputSignal(nameof(Refresh))] public InputSignal CallRefresh;
        public OutputSignal OnSpawnItemRequest;
        public OutputSignal OnRefreshed;
        
        private void Refresh()
        {
            placeholders.Clear();

            placeholders = gameObject.GetComponentsInChildren<PlaceholderComponent>().ToList();

            OnRefreshed.Invoke();
        }

        public void SpawnItemRequest()
        {
            OnSpawnItemRequest.Invoke();
        }

        public bool HasEmptyPlaceholder()
        {
            if (placeholders.Count == 0)
            {
                return false;
            }

            return placeholders.FindAll(placeholder => placeholder.AttachedPlaceable == null).Count > 0;
        }

        public List<PlaceholderComponent> GetEmptyPlaceholders()
        {
            return placeholders.FindAll(placeholder => placeholder.AttachedPlaceable == null);
        }

        public PlaceholderComponent GetPlaceholderByGuid(string guid)
        {
            return placeholders.Find(placeholder => placeholder.Guid == guid);
        }

        public void Clear()
        {
            placeholders.FindAll(placeholder => placeholder.AttachedPlaceable != null)?.ForEach(placeholder =>
            {
                GameObject.Destroy(placeholder.AttachedPlaceable.gameObject);
                placeholder.Detach();
            });
        }
    }
}