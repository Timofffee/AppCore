using System;
using System.Collections.Generic;
using System.Reflection;
using Krem.AppCore.Extensions;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Ports;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Node = UnityEditor.Experimental.GraphView.Node;
using Port = UnityEditor.Experimental.GraphView.Port;

namespace Krem.AppCore.EntityGraph.Views
{
    public class NodeView : Node
    {
        public readonly ICoreNode CoreNodeInstance;
        public readonly List<Port> InputPorts = new List<Port>();
        public readonly List<Port> OutputPorts = new List<Port>();

        public Action<NodeView> OnNodeSelected;
        public Action<NodeView> OnNodeChanged;
        
        public NodeView(ICoreNode coreNodeInstance)
        {
            CoreNodeInstance = coreNodeInstance;

            title = coreNodeInstance.GetType().Name;
            viewDataKey = coreNodeInstance.NodeID;

            style.left = coreNodeInstance.NodePosition.x;
            style.top = coreNodeInstance.NodePosition.y;
            
            InputPorts.Clear();
            OutputPorts.Clear();

            CreateInputComponentPortViews();
            CreateOutputComponentPortViews();
            CreateInputSignalPortViews();
            CreateOutputSignalPortViews();
            CreateInputDataPortViews();
            CreateOutputDataPortViews();
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Vector2 position = new Vector2(newPos.xMin, newPos.yMin);
            CoreNodeInstance.NodePosition = position;
            
            OnNodeChanged?.Invoke(this);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            
            if (OnNodeSelected != null)
            {
                OnNodeSelected.Invoke(this);
            }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent contextEvent)
        {
            contextEvent.menu.AppendAction("Open Script", a =>
            {
                string filePath = ClassFileFinder.FindClassFile(CoreNodeInstance.GetType()).path;
                UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(filePath, 1);
            });
            
            contextEvent.menu.AppendAction("Show Script in Finder", a =>
            {
                string filePath = ClassFileFinder.FindClassFile(CoreNodeInstance.GetType()).path;
                EditorUtility.RevealInFinder(filePath);
            });
        }
        
        private void CreateInputComponentPortViews()
        {
            List<FieldInfo> inputs = CoreNodeInstance.GetPortsAsFields(typeof(InputComponent<>));

            inputs.ForEach(input =>
            {
                Port inputPort = CreatePort(input, Orientation.Horizontal, Direction.Input, input.FieldType.GenericTypeArguments[0], Port.Capacity.Single);
                
                InputPorts.Add(inputPort);
                inputContainer.Add(inputPort);
            });
        }
        
        private void CreateOutputComponentPortViews()
        {
            List<FieldInfo> outputs = CoreNodeInstance.GetPortsAsFields<OutputComponent>();
            
            if (outputs.Count == 0)
            {
                return;
            }
            
            FieldInfo output = outputs[0];
            Port outputPort = CreatePort(output, Orientation.Horizontal, Direction.Output, CoreNodeInstance.GetType(), Port.Capacity.Multi);
            
            OutputPorts.Add(outputPort);
            titleContainer.Add(outputPort);
        }
        
        private void CreateInputSignalPortViews()
        {
            List<FieldInfo> inputs = CoreNodeInstance.GetPortsAsFields<InputSignal>();
            
            inputs.ForEach(input =>
            {
                Port inputPort = CreatePort(input, Orientation.Horizontal, Direction.Input, typeof(InputSignal), Port.Capacity.Multi);
                
                InputPorts.Add(inputPort);
                inputContainer.Add(inputPort);
            });
        }

        private void CreateOutputSignalPortViews()
        {
            List<FieldInfo> outputs = CoreNodeInstance.GetPortsAsFields<OutputSignal>();
            
            outputs.ForEach(output =>
            {
                Port outputPort = CreatePort(output, Orientation.Horizontal, Direction.Output, typeof(OutputSignal), Port.Capacity.Multi);
                
                OutputPorts.Add(outputPort);
                outputContainer.Add(outputPort);
            });
        }
        
        private void CreateInputDataPortViews()
        {
            List<FieldInfo> inputs = CoreNodeInstance.GetPortsAsFields(typeof(InputData<>));

            inputs.ForEach(input =>
            {
                Port inputPort = CreatePort(input, Orientation.Horizontal, Direction.Input, input.FieldType.GenericTypeArguments[0], Port.Capacity.Single);
                
                InputPorts.Add(inputPort);
                inputContainer.Add(inputPort);
            });
        }

        private void CreateOutputDataPortViews()
        {
            List<FieldInfo> outputs = CoreNodeInstance.GetPortsAsFields(typeof(OutputData<>));
            
            outputs.ForEach(output =>
            {
                Port outputPort = CreatePort(output, Orientation.Horizontal, Direction.Output,
                    output.FieldType.GenericTypeArguments[0], Port.Capacity.Multi);
                
                OutputPorts.Add(outputPort);
                outputContainer.Add(outputPort);
            });
        }

        private Port CreatePort(FieldInfo portField, Orientation orientation, Direction direction, Type portType, Port.Capacity portCapacity)
        {
            CorePort corePort = CoreNodeInstance.ConvertFieldInfoToPort(portField) as CorePort;
            Port graphPort = InstantiatePort(orientation, direction, portCapacity, portType);
            graphPort.portName = portField.Name;
            graphPort.viewDataKey = CoreNodeInstance.ConvertFieldInfoToPort(portField).PortID;
            graphPort.portColor = corePort.Color;

            return graphPort;
        }
    }
}
