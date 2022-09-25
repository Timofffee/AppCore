using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.DragMergeMatch.Models
{
    [System.Serializable]
    public class PlaceholderStateModel : Model
    {
        [Header("Data")]
        [SerializeField] protected string _placeholderGuid;
        [SerializeField] protected string _itemGuid;
    
        public string PlaceholderGuid { get => _placeholderGuid; set => SetField(ref _placeholderGuid, value); }
        public string ItemGuid { get => _itemGuid; set => SetField(ref _itemGuid, value); }
    }
}