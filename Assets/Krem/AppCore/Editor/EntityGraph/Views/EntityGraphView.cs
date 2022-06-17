using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Krem.AppCore.Attributes;
using Krem.AppCore.Extensions;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Ports;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph.Views
{
    public sealed class EntityGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<EntityGraphView, UxmlTraits>
        {
        }

        public Action<NodeView> OnNodeSelected;
        public Action<NodeView> OnNodeChanged;
        public Action OnNodeCreated;
        public Action OnNodeDeleted;
        public Action OnEdgeCreated;
        public Action OnEdgeDeleted;

        private CoreEntity _coreGraph;

        // For Content placement on Creating Nodes
        private Vector2 _clickPosition;

        // Manipulators
        private readonly ContentZoomer _contentZoomer = new ContentZoomer();
        private readonly ContentDragger _contentDragger = new ContentDragger();

        private NodeSearchWindow _actionSearchWindow;

        public EntityGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(_contentZoomer);
            this.AddManipulator(_contentDragger);
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = Resources.Load<StyleSheet>("EntityGraph/USS/EntityGraph");
            styleSheets.Add(styleSheet);

            ConfigureSearchWindow();

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            //Debug.Log("UndoRedo EntityGraphView");
        }

        private void ConfigureSearchWindow()
        {
            _actionSearchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
            _actionSearchWindow.Init(CreateNode);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports
                .ToList()
                .Where(endPort => endPort.direction != startPort.direction &&
                                  endPort.node != startPort.node &&
                                  startPort.connections.Count(connection => connection.input == endPort) == 0 &&
                                  startPort.connections.Count(connection => connection.output == endPort) == 0 &&
                                  (
                                      (
                                          startPort.source.GetType().GenericTypeArguments[0].IsSubclassOf(typeof(CorePort)) &&
                                          endPort.source.GetType().GenericTypeArguments[0].IsSubclassOf(typeof(CorePort))
                                      ) ||
                                      startPort.source.GetType() == endPort.source.GetType()
                                  )
                )
                .ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent contextEvent)
        {
            _clickPosition = contextEvent.localMousePosition;

            contextEvent.menu.AppendAction("Search", (a) =>
            {
                SearchWindow.Open(
                    new SearchWindowContext(EditorWindow.focusedWindow.position.position +
                                            a.eventInfo.localMousePosition), _actionSearchWindow);
            });

            var componentTypes = TypeCache.GetTypesDerivedFrom<CoreComponent>();
            foreach (var type in componentTypes)
            {
                if (type.GetCustomAttribute<NodeGraphHiddenAttribute>() != null || type.IsAbstract)
                {
                    continue;
                }

                string groupName = "...";
                if (type.GetCustomAttribute<NodeGraphGroupNameAttribute>() != null)
                    groupName = type.GetCustomAttribute<NodeGraphGroupNameAttribute>().GroupName;

                contextEvent.menu.AppendAction($"Components/{groupName}/{type.Name}", (a) => CreateNode(type));
            }

            var actionTypes = TypeCache.GetTypesDerivedFrom<CoreAction>();
            foreach (var type in actionTypes)
            {
                if (type.GetCustomAttribute<NodeGraphHiddenAttribute>() != null || type.IsAbstract)
                {
                    continue;
                }

                string groupName = "...";
                if (type.GetCustomAttribute<NodeGraphGroupNameAttribute>() != null)
                    groupName = type.GetCustomAttribute<NodeGraphGroupNameAttribute>().GroupName;

                contextEvent.menu.AppendAction($"Actions/{groupName}/{type.Name}", (a) => CreateNode(type));
            }
        }

        public void PopulateView(CoreEntity coreGraph)
        {
            _coreGraph = coreGraph;

            ClearGraph();
            DrawNodes();
            DrawEdges();
        }

        private NodeView FindNodeView(string guid)
        {
            return GetNodeByGuid(guid) as NodeView;
        }

        private void ClearGraph()
        {
            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements.ToList());
            graphViewChanged += OnGraphViewChanged;
        }

        private void DrawNodes()
        {
            _coreGraph.Nodes.ForEach(CreateNodeView);
        }

        private void DrawEdges()
        {
            _coreGraph.Nodes.ForEach(node =>
            {
                node.GetPortsSubclassOf<CoreOutputPort>().ForEach(outputPort =>
                {
                    ((CoreOutputPort) outputPort).Connections.ForEach(inputPort =>
                    {
                        if (inputPort == null)
                        {
                            throw new Exception("Graph Is Broken");
                        }
                        
                        NodeView outputNodeView = FindNodeView(outputPort.ParentID);
                        NodeView inputNodeView = FindNodeView(inputPort.ParentID);

                        Port outputPortView =
                            outputNodeView?.OutputPorts.Find(porView => porView.viewDataKey == outputPort.PortID);
                        Port inputPortView =
                            inputNodeView?.InputPorts.Find(portView => portView.viewDataKey == inputPort.PortID);

                        if (outputPortView == null || inputPortView == null)
                        {
                             outputPortView =
                                 outputNodeView?.OutputPorts.Find(porView => porView.viewDataKey == outputPort.PortID);
                             inputPortView =
                                 inputNodeView?.InputPorts.Find(portView => portView.viewDataKey == inputPort.PortID);

                             if (outputPortView == null || inputPortView == null)
                             {
                                 throw new Exception("Graph Is Broken");
                             }
                        }
                        
                        Edge edge = outputPortView.ConnectTo(inputPortView);
                        AddElement(edge);
                    });
                });
            });
        }

        private void CreateNode(Type type)
        {
            ICoreNode coreNode = _coreGraph.CreateNode(type);

            if (coreNode == null)
            {
                Debug.LogError("Cant Create Node of Type: " + type.Name);

                return;
            }

            // Add actual mouse position
            Vector2 actualPosition = viewTransform.matrix.inverse.MultiplyPoint(_clickPosition);
            coreNode.NodePosition = actualPosition;

            CreateNodeView(coreNode);

            OnNodeCreated?.Invoke();
        }

        private void CreateNodeView(ICoreNode coreNode)
        {
            NodeView nodeView = null;

            if (coreNode is CoreComponent component)
                nodeView = new ComponentNodeView(component, _coreGraph);

            if (coreNode is CoreAction action)
                nodeView = new ActionNodeView(action, _coreGraph);

            if (nodeView == null)
            {
                Debug.LogError("Node View Cannot Be Created");

                return;
            }

            nodeView.OnNodeSelected = OnNodeSelected;
            nodeView.OnNodeChanged = OnNodeChanged;

            AddElement(nodeView);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(element =>
                {
                    if (element is NodeView nodeView)
                    {
                        _coreGraph.RemoveNode(nodeView.CoreNodeInstance);
                        OnNodeDeleted?.Invoke();
                    }

                    if (element is Edge edge)
                    {
                        RemoveEdge(edge);
                        OnEdgeDeleted?.Invoke();
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                CreateEdges(graphViewChange);

                OnEdgeCreated?.Invoke();
            }

            return graphViewChange;
        }

        private void CreateEdges(GraphViewChange graphViewChange)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                NodeView outNodeView = (NodeView) edge.output.node;
                NodeView inputNodeView = (NodeView) edge.input.node;

                if (outNodeView.CoreNodeInstance.GetPortByName(edge.output.portName) == null ||
                    inputNodeView.CoreNodeInstance.GetPortByName(edge.input.portName) == null)
                {
                    Debug.Log("Ports Are Missing");
                    
                    return;
                }
                
                CreateEdge(edge, outNodeView, inputNodeView);
            });
        }

        private void CreateEdge(Edge edge, NodeView outNodeView, NodeView inputNodeView)
        {
            CoreOutputPort outputData =
                outNodeView.CoreNodeInstance.GetPortByName(edge.output.portName) as CoreOutputPort;
            CorePort inputData = 
                inputNodeView.CoreNodeInstance.GetPortByName(edge.input.portName) as CorePort;

            _coreGraph.CreateEdge(outputData, inputData);
        }

        private void RemoveEdge(Edge edge)
        {
            NodeView outputView = edge.output.node as NodeView;
            NodeView inputView = edge.input.node as NodeView;

            CoreOutputPort outputPort =
                outputView?.CoreNodeInstance.GetPortByID(edge.output.viewDataKey) as CoreOutputPort;
            CorePort inputPort = inputView?.CoreNodeInstance.GetPortByID(edge.input.viewDataKey) as CorePort;

            _coreGraph.RemoveEdge(outputPort, inputPort);
        }
    }
}