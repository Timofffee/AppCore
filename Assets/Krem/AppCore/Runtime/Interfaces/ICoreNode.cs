using UnityEngine;

namespace Krem.AppCore.Interfaces
{
    public interface ICoreNode
    {
        public string NodeID { get; }
        public Vector2 NodePosition { get; set; }
    }
}