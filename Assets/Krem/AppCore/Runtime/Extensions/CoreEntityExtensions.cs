using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Krem.AppCore.Interfaces;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Krem.AppCore.Extensions
{
    public static class CoreEntityExtensions
    {
        [CanBeNull]
        public static List<CoreComponent> GetCoreComponents(this CoreEntity coreEntity)
        {
            return coreEntity.gameObject.GetComponents<CoreComponent>().ToList();
        }
        
        public static ICoreNode FindNodeByID(this CoreEntity coreEntity, string id)
        {
            return coreEntity.Nodes.Find(node => node.NodeID == id);
        }
        
        public static ICoreNode FindNodeByType(this CoreEntity coreEntity, Type type)
        {
            return coreEntity.Nodes.Find(node => node.GetType() == type);
        }

        #region Editor Only
        #if UNITY_EDITOR
        
        [CanBeNull]
        public static CoreComponent CreateCoreComponent(this CoreEntity coreEntity, Type type)
        {
            if (type.IsSubclassOf(typeof(CoreComponent)))
            {
                CoreComponent component = Undo.AddComponent(coreEntity.gameObject, type) as CoreComponent;
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
                UndoRecord(coreEntity, "Create Action: " + type.Name);
                coreEntity.Actions.Add(coreAction);
                EditorUtility.SetDirty(coreEntity);

                return coreAction;
            }

            Debug.LogError("Can`t Create CoreAction of Type: " + type.Name);
            
            return null;
        }
        
        public static ICoreNode CreateNode(this CoreEntity coreEntity, Type type)
        {
            if (type.IsSubclassOf(typeof(CoreComponent)))
                return coreEntity.CreateCoreComponent(type);

            if (type.IsSubclassOf(typeof(CoreAction)))
                return coreEntity.CreateCoreAction(type);

            Debug.LogError("Cant Create Node of Type: " + type.Name);

            return null;
        }

        public static void RemoveNode(this CoreEntity coreEntity, ICoreNode coreNode)
        {
            if (coreNode is CoreComponent component)
            {
                Undo.DestroyObjectImmediate(component);

                return;
            }

            if (coreNode is CoreAction action)
            {
                UndoRecord(coreEntity, "Remove Action: " + action.GetType().Name);
                coreEntity.Actions.Remove(action);
                EditorUtility.SetDirty(coreEntity);

                return;
            }

            Debug.LogError("Cant remove Node. Node not found: " + coreNode.GetType().Name);
        }
        
        public static void CreateEdge(this CoreEntity coreEntity, CoreOutputPort output, CorePort input)
        {
            if (output == null)
            {
                Debug.LogError("Add Edge Failed: Cant find OutputPort");
                return;
            }
            
            if (input == null)
            {
                Debug.LogError("Add Edge Failed: Cant find InputPort");
                return;
            }
            
            ICoreNode outputNode = coreEntity.FindNodeByID(output.ParentID);

            if (outputNode.GetType().IsSubclassOf(typeof(CoreComponent)))
            {
                Undo.RecordObject((CoreComponent)outputNode, coreEntity.name + " Add Edge");
                output.Connections.Add(input);
                EditorUtility.SetDirty((CoreComponent)outputNode);
                
                return;
            }

            if (outputNode.GetType().IsSubclassOf(typeof(CoreAction)))
            {
                UndoRecord(coreEntity, "Add Edge");
                output.Connections.Add(input);
                EditorUtility.SetDirty(coreEntity);
                
                return;
            }

            Debug.LogError("Something went wrong when Add Edge");
        }

        public static void RemoveEdge(this CoreEntity coreEntity, CoreOutputPort output, CorePort input)
        {
            if (output == null)
            {
                Debug.LogError("Remove Edge Failed: Cant find OutputPort");
                return;
            }
            
            if (input == null)
            {
                Debug.LogError("Remove Edge Failed: Cant find InputPort");
                return;
            }
            
            ICoreNode outputNode = coreEntity.FindNodeByID(output.ParentID);

            if (outputNode.GetType().IsSubclassOf(typeof(CoreComponent)))
            {
                Undo.RecordObject((CoreComponent)outputNode, coreEntity.name + " Remove Edge");
                output.Connections.Remove(output.Connections.First(port => port.PortID == input.PortID));
                EditorUtility.SetDirty((CoreComponent)outputNode);
                
                return;
            }

            if (outputNode.GetType().IsSubclassOf(typeof(CoreAction)))
            {
                UndoRecord(coreEntity, "Remove Edge");
                output.Connections.Remove(output.Connections.First(port => port.PortID == input.PortID));
                EditorUtility.SetDirty(coreEntity);
                
                return;
            }

            Debug.LogError("Something went wrong when Remove Edge");
        }
        
        public static void SetNodePosition(this CoreEntity coreEntity, ICoreNode node, Vector2 position)
        {
            if (node == null)
            {
                Debug.LogError("Set Node Position Failed: node is null");
                return;
            }

            if (node.GetType().IsSubclassOf(typeof(CoreComponent)))
            {
                Undo.RecordObject((CoreComponent)node, coreEntity.name + " Change Node Position");
                node.NodePosition = position;
                EditorUtility.SetDirty((CoreComponent)node);
                
                return;
            }

            if (node.GetType().IsSubclassOf(typeof(CoreAction)))
            {
                UndoRecord(coreEntity, "Change Node Position");
                node.NodePosition = position;
                EditorUtility.SetDirty(coreEntity);
                
                return;
            }

            Debug.LogError("Something went wrong when Remove Edge");
        }
        
        public static void UndoRecord(this CoreEntity coreEntity, string caption)
        {
            if (Application.isPlaying)
                return;
            
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(coreEntity.gameObject);
            bool isValidPrefabStage = prefabStage != null && prefabStage.stageHandle.IsValid();
            var instanceType = PrefabUtility.GetPrefabAssetType(coreEntity.gameObject);
            bool prefabConnected = PrefabUtility.GetPrefabInstanceStatus(coreEntity.gameObject) == PrefabInstanceStatus.Connected;
            
            Undo.RecordObject(coreEntity, coreEntity.name + " " + caption);
            
            // Is Inside Prefab
            if (isValidPrefabStage)
            {
                Debug.Log("Inside Prefab");
                EditorUtility.SetDirty(coreEntity.gameObject);
            }

            // Prefab On Scene
            if ((instanceType == PrefabAssetType.Regular 
                 || instanceType == PrefabAssetType.Variant
                 || instanceType == PrefabAssetType.Model) && prefabConnected)
            {
                PrefabUtility.RecordPrefabInstancePropertyModifications(coreEntity.gameObject);
            }
            
            // Prefab in Project
            if (instanceType == PrefabAssetType.Regular
                || instanceType == PrefabAssetType.Variant
                || instanceType == PrefabAssetType.Model)
            {
                Debug.Log("Prefab In Project");
                PrefabUtility.ApplyPrefabInstance(coreEntity.gameObject, InteractionMode.UserAction);
            }

            // Game Object On Scene
            if (instanceType == PrefabAssetType.NotAPrefab)
            {
            }
        }

        public static bool CheckGraphIsError(this CoreEntity coreEntity)
        {
            bool isError = false;
            
            // Check Ports parent
            coreEntity.Nodes.ForEach(node =>
            {
                if (node == null)
                {
                    isError = true;
                    return;
                }
                
                node.GetPortsSubclassOf<CorePort>().ForEach(outputPort =>
                {
                    if (outputPort.ParentID != node.NodeID)
                    {
                        isError = true;
                    }
                    
                });
            });


            // Check Actions List
            coreEntity.Actions.ForEach(node =>
            {
                if (node == null)
                {
                    isError = true;
                }
            });

            // Check Connections
            coreEntity.Nodes.ForEach(node =>
            {
                if (node == null)
                {
                    isError = true;
                    return;
                }
                
                node.GetPortsSubclassOf<CoreOutputPort>().ForEach(outputPort =>
                {
                    List<CorePort> newConnections = new List<CorePort>();

                    ((CoreOutputPort) outputPort).Connections.ForEach(inputPort =>
                    {
                        ICoreNode parentNode = coreEntity.FindNodeByID(inputPort.ParentID);
                        ICorePort port = parentNode?.GetPortByID(inputPort.PortID);
                        if (parentNode == null || port == null)
                        {
                            isError = true;
                        }
                    });
                });
            });

            return isError;
        }
        
        public static void FixGraph(this CoreEntity coreEntity)
        {
            bool isFixed = false;
            
            // Fix Ports parent
            coreEntity.Nodes.ForEach(node =>
            {
                if (node == null)
                {
                    return;
                }
                
                node.GetPortsSubclassOf<CorePort>().ForEach(outputPort =>
                {
                    if (outputPort.ParentID != node.NodeID)
                    {
                        outputPort.ParentID = node.NodeID;
                        isFixed = true;
                    }
                    
                });
            });
            if (isFixed)
            {
                Debug.Log("Ports Parents Is Fixed");
                EditorUtility.SetDirty(coreEntity.gameObject);
                //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }


            // Fix Actions List
            isFixed = false;
            List<CoreAction> fixedActionsList = new List<CoreAction>();
            coreEntity.Actions.ForEach(node =>
            {
                if (node == null)
                {
                    isFixed = true;
                }
                else
                {
                    fixedActionsList.Add(node);
                }
            });

            if (isFixed)
            {
                coreEntity.Actions = fixedActionsList;
                Debug.Log("Action Graph Is Fixed");
                EditorUtility.SetDirty(coreEntity.gameObject);
                //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            // Fix Connections
            coreEntity.Nodes.ForEach(node =>
            {
                node.GetPortsSubclassOf<CoreOutputPort>().ForEach(outputPort =>
                {
                    isFixed = false;
                    List<CorePort> newConnections = new List<CorePort>();

                    ((CoreOutputPort) outputPort).Connections.ForEach(inputPort =>
                    {
                        ICoreNode parentNode = coreEntity.FindNodeByID(inputPort.ParentID);
                        ICorePort port = parentNode?.GetPortByID(inputPort.PortID);
                        if (parentNode == null || port == null)
                        {
                            isFixed = true;
                        }
                        else
                        {
                            newConnections.Add(inputPort);
                        }
                    });

                    if (isFixed)
                    {
                        ((CoreOutputPort) outputPort).Connections = newConnections;
                        Debug.Log("Connection Graph Is Fixed");
                        EditorUtility.SetDirty(coreEntity.gameObject);
                        //EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    }
                });
            });

            coreEntity.GraphIsBrokenState = false;
        }
        
        #endif
        #endregion
    }
}