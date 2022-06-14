using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Krem.AppCore.Interfaces;

namespace Krem.AppCore.Extensions
{
    public static class CoreNodeExtensions
    {
        public static List<FieldInfo> GetPortsAsFields(this ICoreNode instance)
        {
            Type componentType = instance.GetType();
            List<FieldInfo> fields = componentType.GetFields(BindingFlags.Instance |  BindingFlags.Public).ToList();
            
            List<FieldInfo> ports = fields.FindAll(item => item.FieldType.GetInterfaces().Contains(typeof(ICorePort)));
            
            return ports;
        }

        public static List<FieldInfo> GetPortsAsFields<T>(this ICoreNode instance) where T : ICorePort
        {
            List<FieldInfo> fields = GetPortsAsFields(instance).FindAll(item => item.FieldType == typeof(T));
            
            return fields;
        }
        
        public static List<FieldInfo> GetPortsAsFieldsSubclassOf<T>(this ICoreNode instance) where T : ICorePort
        {
            List<FieldInfo> fields = GetPortsAsFields(instance).FindAll(item => item.FieldType.IsSubclassOf(typeof(T)));
            
            return fields;
        }
        
        public static List<FieldInfo> GetPortsAsFields(this ICoreNode instance, Type type)
        {
            List<FieldInfo> fields = GetPortsAsFields(instance).FindAll(item => item.FieldType.Name == type.Name);
            
            return fields;
        }

        public static List<ICorePort> GetPorts(this ICoreNode instance)
        {
            List<FieldInfo> ports = instance.GetPortsAsFields();
            
            return instance.ConvertFieldsToPorts(ports);
        }

        public static List<ICorePort> GetPorts<T>(this ICoreNode instance) where T : ICorePort
        {
            List<FieldInfo> ports = instance.GetPortsAsFields<T>();
            
            return instance.ConvertFieldsToPorts(ports);
        }
        
        public static List<ICorePort> GetPortsSubclassOf<T>(this ICoreNode instance) where T : ICorePort
        {
            List<FieldInfo> ports = instance.GetPortsAsFieldsSubclassOf<T>();
            
            return instance.ConvertFieldsToPorts(ports);
        }

        [CanBeNull]
        public static ICorePort GetPortByName(this ICoreNode instance, string portName)
        {
            FieldInfo portFieldInfo = instance.GetPortsAsFields()?.FirstOrDefault(port => port.Name == portName);

            if (portFieldInfo == null)
            {
                return null;
            }

            return instance.ConvertFieldInfoToPort(portFieldInfo);
        }

        [CanBeNull]
        public static ICorePort GetPortByID(this ICoreNode instance, string portID)
        {
            ICorePort corePort = instance.GetPorts()?.Find(port => port.PortID == portID);

            return corePort;
        }

        public static void RefreshPortsParent(this ICoreNode instance)
        {
            List<ICorePort> ports = instance.GetPorts();

            ports.ForEach(port =>
            {
                port.ParentID = instance.NodeID;
            });
        }

        [CanBeNull]
        public static ICorePort ConvertFieldInfoToPort(this ICoreNode instance, FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                return null;
            }

            ICorePort result = (ICorePort) fieldInfo.GetValue(instance);
             if (result == null)
             {
                 result = Activator.CreateInstance(fieldInfo.FieldType) as ICorePort;
                 fieldInfo.SetValue(instance, result);
             }
            
            return (ICorePort) fieldInfo.GetValue(instance);
        }

        private static List<ICorePort> ConvertFieldsToPorts(this ICoreNode instance, List<FieldInfo> fieldsInfo)
        {
            List<ICorePort> ports = new List<ICorePort>();
            
            fieldsInfo.ForEach(field =>
            {
                ports.Add(instance.ConvertFieldInfoToPort(field));
            });

            return ports;
        }
    }
}