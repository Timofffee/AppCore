using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM
{
    public static class JsonStorageService<TModel> where TModel : Model, new()
    {
        private const string FileExtension = "json";

        public static bool Save(string name, TModel model)
        {
            string json = JsonUtility.ToJson(model);
            File.WriteAllText(GetStoragePath(name), json);
            
            return true;
        }
        
        public static bool Save(string name, List<TModel> list)
        {
            Collection<TModel> collection = new Collection<TModel>();
            collection.Data = list;
            
            string json = JsonUtility.ToJson(collection);
            File.WriteAllText(GetStoragePath(name), json);
            
            return true;
        }
        
        public static bool Load(string name, out TModel model)
        {
            model = new TModel();

            if (!File.Exists(GetStoragePath(name)))
            {
                Debug.LogWarning("No Stored data to Load: " + name);
                
                return false;
            }
                

            string json = File.ReadAllText(GetStoragePath(name));
            JsonUtility.FromJsonOverwrite(json, model);

            return true;
        }

        public static bool Load(string name, out List<TModel> list)
        {
            Collection<TModel> collection = new Collection<TModel>();
            list = new List<TModel>();

            if (!File.Exists(GetStoragePath(name)))
            {
                Debug.LogWarning("No Stored data to Load: " + name);
                
                return false;
            }
                

            string json = File.ReadAllText(GetStoragePath(name));
            JsonUtility.FromJsonOverwrite(json, collection);

            list = collection.Data;

            return true;
        }

        public static bool Delete(string name)
        {
            if (!File.Exists(GetStoragePath(name)))
            {
                Debug.LogWarning("No Stored data to Delete: " + name);
                
                return false;
            }
            
            File.Delete(GetStoragePath(name));

            return true;
        }
        
        public static string GetStoragePath(string name)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + name + "." + FileExtension;
        }
    }
}
