using Krem.DragMergeMatch.Components;
using UnityEditor;
using UnityEngine;

namespace Krem.DragMergeMatch.Editor
{
    [CustomEditor(typeof(PlaceholderComponent), true)]
    public class PlaceholderComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Regenerate GUID", GUILayout.Height(40)))
            {
                ((PlaceholderComponent) target).RegenerateGuid();
            }
        }
    }
}
