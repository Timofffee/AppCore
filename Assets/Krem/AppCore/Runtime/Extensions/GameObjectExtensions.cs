using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Krem.AppCore.Extensions
{
    public static class GameObjectExtensions
    {

        public static List<T> GetComponentsWithInterface<T>(this GameObject src)
        {
            return src.GetComponents<T>().ToList();
        }

        public static object[] GetComponentsWithInterface(this GameObject src, Type type)
        {
            Type goType = src.GetType();

            MethodInfo getComponentMethodInfo = goType
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(m => m.Name == nameof(src.GetComponents) && m.IsGenericMethod)
                .Single(m => m.GetParameters().Length == 0);
            
            MethodInfo generic = getComponentMethodInfo.MakeGenericMethod(type);
            
            return generic.Invoke(src, null) as object[];
        }
    }
}