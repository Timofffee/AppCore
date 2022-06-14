using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Editor
{
    [CustomEditor(typeof(ScriptableCollection<>), true)]
    public class ScriptableListEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (((IScriptableRepository) target).IsStorable())
            {
                GUILayout.Space(40);

                if (GUILayout.Button("Save", GUILayout.Height(40)))
                {
                    ((IScriptableRepository) target).Save();
                }

                if (GUILayout.Button("Load", GUILayout.Height(40)))
                {
                    ((IScriptableRepository) target).Load();
                }
                
                if (GUILayout.Button("Delete Stored", GUILayout.Height(40)))
                {
                    ((IScriptableRepository) target).Delete();
                }
            }
        }
    }
}
