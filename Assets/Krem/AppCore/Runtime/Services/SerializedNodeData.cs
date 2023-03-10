using System;
using System.Reflection;
using Krem.AppCore.Interfaces;
using UnityEngine;

namespace Krem.AppCore.Services
{
    [Serializable]
    public class SerializedNodeData
    {
        public string NodeTypeName;
        public string AssemblyName;
        public string SerializedData;

        public SerializedNodeData(ICoreNode node)
        {
            NodeTypeName = node.GetType().FullName;
            AssemblyName = node.GetType().Assembly.GetName().Name;
            SerializedData = JsonUtility.ToJson(node);
        }

        public ICoreNode Deserialize()
        {
            try
            {
                // For Net Framework
                //ICoreNode result = Activator.CreateInstance(AssemblyName, NodeTypeName).Unwrap() as ICoreNode;
                
                Assembly assembly = Assembly.Load(AssemblyName);
                Type type = assembly.GetType(NodeTypeName);
                ICoreNode result = Activator.CreateInstance(type) as ICoreNode;
                JsonUtility.FromJsonOverwrite(SerializedData, result);
                
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                Debug.LogError("Node: " + NodeTypeName + " In Assembly: " + AssemblyName + " Is Lost");
            }

            return null;
        }
    }
}