using UnityEditor;
using UnityEngine;

namespace Krem.AppCore.Tools
{
    public class CreateEntity : MonoBehaviour
    {
        [MenuItem("Krem/AppCore/Create Entity #%E")]
        public static void CreateEntityInstance()
        {
            GameObject entity = Resources.Load("Entity") as GameObject;
            GameObject parent = Selection.activeGameObject;

            GameObject instance = Instantiate(entity, parent?.transform);
            instance.transform.position = Vector3.zero;
            instance.name = "Entity";
            
            Undo.RegisterCreatedObjectUndo(instance, "Add Entity");
        }
    }
}