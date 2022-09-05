using UnityEditor;
using UnityEngine;
using Krem.AppCore.Extensions;

namespace Krem.AppCore.EntityGraph
{
    [CustomEditor(typeof(CoreEntity), true)]
    public class EntityEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GUILayout.Space(20);
            
            if (GUILayout.Button("Open Node Editor", GUILayout.Height(60)))
            {
                EntityGraph.ShowEntityGraph();
            }
            
            if (GUILayout.Button("Fix Graph", GUILayout.Height(60)))
            {
                ((CoreEntity)target).FixGraph();
            }

            GUILayout.Space(20);
            
            base.OnInspectorGUI();
        }
    }
}
