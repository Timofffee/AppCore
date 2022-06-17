using System.Collections.Generic;

namespace Krem.AppCore.Interfaces
{
    public interface ICoreGraph
    {
        public List<ICoreNode> Nodes { get; }
    }
}