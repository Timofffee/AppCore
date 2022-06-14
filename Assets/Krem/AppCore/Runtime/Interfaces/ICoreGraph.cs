using System;
using System.Collections.Generic;

namespace Krem.AppCore.Interfaces
{
    public interface ICoreGraph
    {
        public List<ICoreNode> Nodes { get; }
        
        public ICoreNode CreateNode(Type type);
        public void RemoveNode(ICoreNode coreNode);

        public void SetDirty();
    }
}