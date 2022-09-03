using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Krem.AppCore.Attributes;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Extensions;
using Krem.AppCore.Ports;
using Krem.AppCore.Services;
using UnityEngine;

namespace Krem.AppCore
{
    [DisallowMultipleComponent]
    public sealed class CoreEntity : MonoBehaviour, ICoreGraph, ISerializationCallbackReceiver
    {
        private List<CoreAction> _actions = new List<CoreAction>();
        [SerializeField] private List<SerializedNodeData> _serializedActions = new List<SerializedNodeData>();
        [SerializeField, InspectorReadOnly] private bool _graphIsBroken = false;

        public List<ICoreNode> Nodes
        {
            get
            {
                List<ICoreNode> nodes = new List<ICoreNode>();
                nodes.AddRange(this.GetCoreComponents());
                nodes.AddRange(Actions);

                return nodes;
            }
        }

        public List<CoreComponent> Components
        {
            get => this.GetCoreComponents();
        }

        public List<CoreAction> Actions
        {
            get => _actions;
            set => _actions = value;
        }

        public bool GraphIsBrokenState
        {
            get => _graphIsBroken;
            set => _graphIsBroken = value;
        }

        private void Awake()
        {
            if (this.CheckGraphIsError() || GraphIsBrokenState)
            {
                GraphIsBrokenState = true;
                throw new Exception("Entity: " + this + " Graph is Broken");
            }

            CheckComponentsForNullReferences();
            BindOutputData();
            BindInputSignals();
            BindOutputSignals();
            BindOutputComponents();
            InjectDependency();
        }

        public void OnBeforeSerialize()
        {
            if (GraphIsBrokenState)
            {
                return;
            }
            
            Serialize();
        }

        public void OnAfterDeserialize()
        {
            if (GraphIsBrokenState)
            {
                return;
            }
            
            Deserialize();
        }

        public void Serialize()
        {
            _serializedActions.Clear();

            for (int i = 0; i < _actions.Count; i++)
            {
                if (_actions[i] != null)
                {
                    _serializedActions.Add(new SerializedNodeData(_actions[i]));
                }
            }
        }

        public void Deserialize()
        {
            _actions.Clear();

            for (int i = 0; i < _serializedActions.Count(); i++)
            {
                CoreAction action = (CoreAction) _serializedActions[i].Deserialize();

                if (action != null)
                {
                    _actions.Add((CoreAction) _serializedActions[i].Deserialize());
                }
            }
        }

        private void BindInputSignals()
        {
            Nodes.ForEach(node =>
            {
                List<FieldInfo> inputPorts = node.GetPortsAsFields<InputSignal>();

                inputPorts.ForEach(port =>
                {
                    if (port.GetCustomAttribute<BindInputSignalAttribute>() != null)
                    {
                        BindInputSignalAttribute bindInputSignalAttribute =
                            port.GetCustomAttribute<BindInputSignalAttribute>();
                        MethodInfo methodInfo = node.GetType().GetMethod(bindInputSignalAttribute.BindedMethodName,
                            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                        InputSignal inputSignal = node.ConvertFieldInfoToPort(port) as InputSignal;
                        inputSignal.InvokeAction = (Action) Delegate.CreateDelegate(typeof(Action), node, methodInfo);
                    }
                });
            });
        }

        private void BindOutputSignals()
        {
            Nodes.ForEach(node =>
            {
                List<FieldInfo> outputPorts = node.GetPortsAsFields<OutputSignal>();

                outputPorts.ForEach(port =>
                {
                    OutputSignal outputSignalPort = node.ConvertFieldInfoToPort(port) as OutputSignal;

                    outputSignalPort.Connections.ForEach(inputSignalPort =>
                    {
                        ICoreNode coreNodeInstance = this.FindNodeByID(inputSignalPort.ParentID);
                        InputSignal inputSignalInstance =
                            coreNodeInstance.GetPortByID(inputSignalPort.PortID) as InputSignal;
                        outputSignalPort.InvocationList += inputSignalInstance.Invoke;
                    });
                });
            });
        }
        
        private void BindOutputComponents()
        {
            Components.ForEach(component =>
            {
                List<FieldInfo> outputPorts = component.GetPortsAsFields<OutputComponent>();

                if (outputPorts.Count == 0)
                {
                    return;
                }

                outputPorts.ForEach(port =>
                {
                    OutputComponent outputComponentPort = component.ConvertFieldInfoToPort(port) as OutputComponent;

                    outputComponentPort?.Connections.ForEach(inputComponentPort =>
                    {
                        ICoreNode coreNodeInstance = this.FindNodeByID(inputComponentPort.ParentID);
                        
                        CorePort inputComponentInstance =
                            coreNodeInstance.GetPortByID(inputComponentPort.PortID) as CorePort;

                        var bindComponent = inputComponentInstance?.GetType().GetField("Component");
                        bindComponent.SetValue(inputComponentInstance, component);
                    });
                });
            });
        }
        
        private void BindOutputData()
        {
            Components.ForEach(component =>
            {
                List<FieldInfo> outputPorts = component.GetPortsAsFieldsSubclassOf<CoreOutputPort>().
                    FindAll(port => port.FieldType.IsGenericType && port.FieldType.GetGenericTypeDefinition() == typeof(OutputData<>));

                if (outputPorts.Count == 0)
                {
                    return;
                }
                
                outputPorts.ForEach(port =>
                {
                    CoreOutputPort outputDataPort = component.ConvertFieldInfoToPort(port) as CoreOutputPort;

                    outputDataPort?.Connections.ForEach(inputDataPort =>
                    {
                        ICoreNode coreNodeInstance = this.FindNodeByID(inputDataPort.ParentID);
                        
                        CorePort inputDataInstance =
                            coreNodeInstance.GetPortByID(inputDataPort.PortID) as CorePort;


                        FieldInfo dataToBind = outputDataPort.GetType().GetField("Data", BindingFlags.Instance | BindingFlags.Public);
                        
                        MethodInfo bindMethod = inputDataInstance?.GetType().GetMethod("Bind", BindingFlags.Instance | BindingFlags.Public);
                        bindMethod.Invoke(inputDataInstance, new object[] {outputDataPort});
                    });
                });
            });
        }

        private void InjectDependency()
        {
            Actions.ForEach(action =>
            {
                List<FieldInfo> fields = action.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
                
                fields = fields.FindAll(field => field.GetCustomAttribute<InjectComponentAttribute>() != null);

                fields.ForEach(field =>
                {
                    if (field.FieldType.IsSubclassOf(typeof(CoreComponent)))
                    {
                        CoreComponent injection = this.FindNodeByType(field.FieldType) as CoreComponent;

                        if (injection == null)
                        {
                            throw new Exception("Entity Dependency Injection Failed. Entity: " + this +
                                                "has no valid node: " + field.FieldType + " for Action: " + action.GetType());
                        }

                        field.SetValue(action, injection);
                    }
                    else if (field.FieldType.IsSubclassOf(typeof(Component)))
                    {
                        if (this.TryGetComponent(field.FieldType, out Component injection) == false)
                        {
                            throw new Exception("Entity Dependency Injection Failed. Entity: " + this +
                                                "has no valid node: "  + field.FieldType + " for Action: " + field.Name);
                        }

                        field.SetValue(action, injection);
                    }
                });
            });
        }

        private void CheckComponentsForNullReferences()
        {
            Components.ForEach(component =>
            {
                List<FieldInfo> fields = component.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();

                fields = fields.FindAll(field => field.GetCustomAttribute<NotNullAttribute>() != null);
                
                fields.ForEach(field =>
                {
                    if (field.GetValue(component).Equals(null))
                    {
                        throw new ArgumentNullException("field:  " + field.Name);
                    }
                });
            });
        }
    }
}