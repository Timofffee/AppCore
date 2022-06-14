using System;

namespace Krem.AppCore.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NodeGraphGroupNameAttribute : Attribute
    {
        public string GroupName;
        
        public NodeGraphGroupNameAttribute(string groupName)
        {
            GroupName = groupName;
        }
    }
}
