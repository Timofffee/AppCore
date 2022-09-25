using System.Reflection;
using Krem.AppCore.Extensions;
using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Editor
{
    [CustomPropertyDrawer(typeof(Model), true)]
    public class ModelEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
            
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            
            if (targetObjectClassType.IsSubclassOfRawGeneric(typeof(ScriptableModel<>)))
            {
                if (GUILayout.Button("Regenerate GUID", GUILayout.Height(40)))
                {
                
                    var field = targetObjectClassType.GetField(property.propertyPath, BindingFlags.Default | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                
                    if (field != null)
                    {
                        var value = field.GetValue(targetObject);
                        (value as IHaveGuid)?.RegenerateGuid();
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }

            
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}