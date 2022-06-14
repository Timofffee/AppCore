using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Krem.AppCore.Extensions
{
    public static class CoreEntityExtensions
    {
        [CanBeNull]
        public static List<CoreComponent> GetCoreComponents(this CoreEntity instance)
        {
            return instance.gameObject.GetComponents<CoreComponent>().ToList();
        }

        [CanBeNull]
        public static CoreComponent CreateCoreComponent(this CoreEntity coreEntity, Type type)
        {
            if (type.IsSubclassOf(typeof(CoreComponent)))
            {
                CoreComponent component = coreEntity.gameObject.AddComponent(type) as CoreComponent;
                component.RefreshPortsParent();
                
                return component;
            }
            
            Debug.LogError("Can`t Create CoreComponent of Type: " + type.Name);
            
            return null;
        }

        [CanBeNull]
        public static CoreAction CreateCoreAction(this CoreEntity coreEntity, Type type)
        {
            if (type.IsSubclassOf(typeof(CoreAction)))
            {
                CoreAction coreAction = Activator.CreateInstance(type) as CoreAction;
                coreAction.RefreshPortsParent();
                coreEntity.Actions.Add(coreAction);

                return coreAction;
            }

            Debug.LogError("Can`t Create CoreAction of Type: " + type.Name);
            
            return null;
        }
    }
}