using System;
using Krem.AppCore.Interfaces;

namespace Krem.AppCore.Extensions
{
    public static class CoreGraphExtensions
    {
        public static ICoreNode FindNodeByID(this ICoreGraph coreGraph, string id)
        {
            return coreGraph.Nodes.Find(node => node.NodeID == id);
        }
        
        public static ICoreNode FindNodeByType(this ICoreGraph coreGraph, Type type)
        {
            return coreGraph.Nodes.Find(node => node.GetType() == type);
        }
    }
}