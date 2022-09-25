using System;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.AppCore
{
    [RequireComponent(typeof(CoreEntity))]
    public abstract class CoreComponent : MonoBehaviour, ICoreNode
    {
        [SerializeField] protected string _nodeID;
        [HideInInspector][SerializeField] protected Vector2 _nodePosition;

        public OutputComponent This;

        // Node fields
        public string NodeID { get => _nodeID; }
        public Vector2 NodePosition { get => _nodePosition; set => _nodePosition = value; }

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            if (string.IsNullOrEmpty(_nodeID))
            {
                _nodeID = Guid.NewGuid().ToString();
            }
#endif
        }
        
    }
}