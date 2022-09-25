using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.DragMergeMatch.Models
{
    [System.Serializable]
    public class DragMergeItemModel : Model
    {
        [Header("Data")]
        [SerializeField] protected GameObject _itemPrefab;
        [SerializeField] protected GameObject _itemShadowClone;

        [Header("Settings")]
        public Vector3 placeOffset = new Vector3(0,0,0);
        public bool useShadowCloneToDrag = false;
        public Vector3 dragOffset = new Vector3(0, 0,  -1);
        
        public GameObject ItemPrefab { get => _itemPrefab; }
        public GameObject ItemShadowClone { get => _itemShadowClone; }
    }
}