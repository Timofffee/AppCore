using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Krem.AppCore.Attributes;

namespace Krem.AppCore.Extensions
{
    public static class CoreActionExtensions
    {
        public static List<FieldInfo> GetParametersAsFields(this CoreAction instance)
        {
            Type componentType = instance.GetType();
            List<FieldInfo> fields = componentType.GetFields(BindingFlags.Instance |  BindingFlags.Public).ToList();
            
            List<FieldInfo> parameters = fields.FindAll(item => item.GetCustomAttribute<ActionParameterAttribute>() != null);
            
            return parameters;
        }
    }
}