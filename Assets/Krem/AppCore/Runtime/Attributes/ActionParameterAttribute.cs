using System;

namespace Krem.AppCore.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ActionParameterAttribute: Attribute
    {
        public ActionParameterAttribute()
        {
        }
    }
}