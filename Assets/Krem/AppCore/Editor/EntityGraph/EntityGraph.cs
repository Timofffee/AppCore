using Krem.AppCore.EntityGraph.Views;
using Krem.AppCore.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Krem.AppCore.EntityGraph
{
    public sealed class EntityGraph : EditorWindow
    {
        private EntityGraphView _entityGraphView;
        private InspectorView _inspectorView;
        private VisualElement _graphIsBrokenCaptionView;
        
        private CoreEntity _selectedEntity;
        
        [MenuItem("Krem/AppCore/Show EntityGraph #E")]
        public static void ShowEntityGraph()
        {
            EntityGraph wnd = GetWindow<EntityGraph>();

            if (wnd == null)
            {
                wnd.Close();
                wnd = GetWindow<EntityGraph>();
            }

            wnd.titleContent = new GUIContent("EntityGraph");
        }

        public void CreateGUI()
        {
            rootVisualElement.Clear();
            
            CoreEntity coreEntity = (Selection.activeObject as GameObject)?.GetComponent<CoreEntity>();
            if (coreEntity == null)
            {
                Debug.LogWarning($"Entity: {this} is not selected");
                return;
            }
            
            _selectedEntity = coreEntity;
            
            if (coreEntity.CheckGraphIsError() || coreEntity.GraphIsBrokenState)
            {
                coreEntity.GraphIsBrokenState = true;
                Debug.LogError($"Entity: {this} Graph is Broken");
            }

            CreateGraphGUI(_selectedEntity);
        }

        private void OnUndoRedo()
        {
            CreateGUI();
        }

        private void OnSelectionChange()
        {
            CreateGUI();
        }

        private void OnProjectChange()
        {
            CreateGUI();
        }

        private void CreateGraphGUI(CoreEntity coreEntity)
        {
            VisualElement root = rootVisualElement;
            VisualTreeAsset visualTree = Resources.Load<VisualTreeAsset>("EntityGraph/EntityGraph");
            visualTree.CloneTree(root);
            
            StyleSheet styleSheet = Resources.Load<StyleSheet>("EntityGraph/USS/EntityGraph");
            root.styleSheets.Add(styleSheet);
            
            _entityGraphView = root.Q<EntityGraphView>();
            _inspectorView = root.Q<InspectorView>();
            _inspectorView.SelectedEntity = _selectedEntity;
            _graphIsBrokenCaptionView = root.Q("BrokenGraphCaption");
            
            _graphIsBrokenCaptionView.visible = false;
            if (_selectedEntity.GraphIsBrokenState)
            {
                _graphIsBrokenCaptionView.visible = true;
                return;
            }

            BindEvents();
            PopulateEntityGraph();
        }

        private void BindEvents()
        {
            _entityGraphView.OnNodeSelected = OnNodeSelectionChanged;
            _entityGraphView.OnNodeCreated = OnNodeCreated;
            _entityGraphView.OnNodeDeleted = OnNodeDeleted;
            _entityGraphView.OnNodeChanged = OnNodeChanged;
            _entityGraphView.OnEdgeCreated = OnEdgeCreated;
            _entityGraphView.OnEdgeDeleted = OnEdgeDeleted;
            _inspectorView.OnActionNodeValueChanged = OnActionValueChanged;
            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void PopulateEntityGraph()
        {
            _entityGraphView.PopulateView(_selectedEntity);
        }

        private void OnNodeSelectionChanged(NodeView nodeView)
        {
            _inspectorView.UpdateNodeSelection(nodeView);
        }
        
        private void OnNodeCreated()
        {
            //_selectedEntity.SetDirty();
        }

        private void OnNodeDeleted()
        {
            _inspectorView.ClearInspector();
            
            //_selectedEntity.SetDirty();
        }

        private void OnNodeChanged(NodeView nodeView)
        {

        }

        private void OnActionValueChanged(NodeView nodeView)
        {
            Debug.Log("OnActionParameterChanged");
        }

        private void OnEdgeCreated()
        {
        }

        private void OnEdgeDeleted()
        {
        }
    }
}