using System;

namespace Krem.AppCore.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class BindInputSignalAttribute: Attribute
    {
        public string BindedMethodName;

        public BindInputSignalAttribute(string bindMethodName)
        {
            BindedMethodName = bindMethodName;
        }
    }
}