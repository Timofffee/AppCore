using System.Collections.Generic;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace Krem.DragMergeMatch.Scriptables
{
    [CreateAssetMenu(fileName = "NewItemsRepository", menuName = "Drag Merge Match/Create Items Repository", order = 0)]
    public class ItemsRepository : ScriptableObject
    {
        public List<DragMergeScriptableModel> Data;

        public DragMergeScriptableModel FindByGuid(string guid)
        {
            return Data.Find(item => item.Model.Guid == guid);
        }

        public DragMergeScriptableModel FindNextByGuid(string guid)
        {
            int index = Data.FindIndex(item => item.Model.Guid == guid);

            if (index >= Data.Count - 1 || index < 0)
            {
                return null;
            }

            return Data[index + 1];
        }
    }
}
