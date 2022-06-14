using System;
using System.Reflection;
using Krem.AppCore.Extensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Krem.AppCore.EntityGraph.Views
{
    public sealed class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        public Action<NodeView> OnNodeValueChanged;
        
        private Editor _editor;
        private NodeView _selectedNode;
        
        public InspectorView()
        {
        }

        public void UpdateNodeSelection(NodeView nodeView)
        {
            if (nodeView is ComponentNodeView componentNodeView)
            {
                UpdateComponentSelection(componentNodeView);
            }

            if (nodeView is ActionNodeView actionNodeView)
            {
                UpdateActionSelection(actionNodeView);
            }
        }

        public void ClearInspector()
        {
            Clear();
            _selectedNode = null;
            Object.DestroyImmediate(_editor);
        }
        
        private void UpdateComponentSelection(ComponentNodeView nodeView)
        {
            ClearInspector();
            
            _selectedNode = nodeView;

            _editor = Editor.CreateEditor((Object)nodeView.CoreNodeInstance);
            IMGUIContainer container = new IMGUIContainer(() =>
            {
                _editor.OnInspectorGUI();
            });
            
            Add(container);
        }
        
        private void UpdateActionSelection(ActionNodeView nodeView)
        {
            ClearInspector();
            
            _selectedNode = nodeView;

            CoreAction action = (CoreAction)nodeView.CoreNodeInstance;
            
            action.GetParametersAsFields().ForEach(parameterField =>
            {
                CreateFieldView(parameterField, action);
            });
        }

        private void CreateFieldView(FieldInfo parameterField, CoreAction action)
        {
            if (parameterField.FieldType == typeof(float))
            {
                TextValueField<float> fieldView = new FloatField(parameterField.Name);
                fieldView.value = (float) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
                
            if (parameterField.FieldType == typeof(int))
            {
                var fieldView = new IntegerField(parameterField.Name);
                fieldView.value = (int) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
                
            if (parameterField.FieldType == typeof(string))
            {
                var fieldView = new TextField(parameterField.Name);
                fieldView.value = (string) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
            
            if (parameterField.FieldType == typeof(bool))
            {
                var fieldView = new Toggle(parameterField.Name);
                fieldView.value = (bool) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
            
            if (parameterField.FieldType == typeof(Vector2))
            {
                var fieldView = new Vector2Field(parameterField.Name);
                fieldView.value = (Vector2) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
            
            if (parameterField.FieldType == typeof(Vector3))
            {
                var fieldView = new Vector3Field(parameterField.Name);
                fieldView.value = (Vector3) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
            
            if (parameterField.FieldType.BaseType == typeof(Enum))
            {
                var fieldView = new EnumField((Enum)parameterField.GetValue(action));
                fieldView.label = parameterField.Name;
                fieldView.value = (Enum) parameterField.GetValue(action);
                fieldView.RegisterValueChangedCallback(value =>
                {
                    parameterField.SetValue(action, value.newValue);
                    OnNodeValueChanged?.Invoke(_selectedNode);
                });
                Add(fieldView);
            }
        }
    }
}
