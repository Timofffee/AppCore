using Krem.DragMergeMatch.Components;
using UnityEditor;
using UnityEngine;

namespace Krem.DragMergeMatch.Editor
{
    [InitializeOnLoad]
    public class RegeneratePlaceholderGuid
    {
        private static bool _isNewPlaceholderComponentAdded = false;
        
        static RegeneratePlaceholderGuid()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            GameObject ob = EditorUtility.InstanceIDToObject(Selection.activeInstanceID) as GameObject;

            if (_isNewPlaceholderComponentAdded && ob != null && ob.TryGetComponent<PlaceholderComponent>(out var component))
            {
                component.RegenerateGuid();
            }
            
            _isNewPlaceholderComponentAdded = false;
        }
    
        private static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            // ignore items which are not selected
            if (Selection.activeInstanceID != instanceID)
                return;


            Event e = Event.current;
            if (e.type == EventType.ValidateCommand || e.type == EventType.ExecuteCommand)
            {
                var o = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (o != null)
                {
                    if (e.commandName == "Duplicate" && o.GetComponent<PlaceholderComponent>() != null)
                    {
                        _isNewPlaceholderComponentAdded = true;
                    }
                }
            }
        }
    }
}