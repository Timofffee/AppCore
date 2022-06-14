using System;
using Krem.AppCore.Attributes;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.AppCore
{
    [Serializable]
    public abstract class CoreAction : ICoreNode
    {
        [BindInputSignal(nameof(ExecuteAction))] public InputSignal Execute;
        public OutputSignal OnExecuted;

        
        [SerializeField] protected string _nodeID;
        [SerializeField] protected Vector2 _nodePosition;

        public string NodeID { get => _nodeID; }
        public Vector2 NodePosition { get => _nodePosition; set => _nodePosition = value; }

        public CoreAction()
        {
            _nodeID = Guid.NewGuid().ToString();
        }

        public void ExecuteAction()
        {
            if (Action())
                OnExecuted.Invoke();
        }

        protected abstract bool Action();
    }
}