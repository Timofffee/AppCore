using System;
using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM
{
    public abstract class ScriptableModel<TModel> : ScriptableObject, IScriptableRepository where TModel : Model, new()
    {
        [Header("Settings")]
        [SerializeField] protected bool storable = true;
        [SerializeField] protected bool immutable = false;
        [SerializeField] protected bool dataProtected = false;
        
        [Header("Data")]
        [SerializeField] protected TModel data = new TModel();

        public event Action OnLoaded;
        public event Action OnSaved;
        public event Action OnSavesDeleted;
        public event Action OnSetData;
        
        public TModel Model
        {
            get
            {
                if (immutable)
                {
                    return (TModel)Clone();
                }
                
                return data;
            }
            set
            {
                if (dataProtected)
                {
                    Debug.LogError("Try to set data when data protected " + name);
                    
                    return;
                }
                
                data = (TModel) (immutable ? value.Clone<TModel>() : value);

                OnSetData?.Invoke();
            }
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(data.Guid))
            {
                data.RegenerateGuid();
            }
        }

        public bool IsStorable()
        {
            return storable;
        }
        
        public bool Load()
        {
            if (storable == false)
            {
                Debug.LogError(name + " Not Storable");
                
                return false;
            }
            
            if (JsonStorageService<TModel>.Load(name, out TModel loadedData) == false)
                return false;

            data = loadedData;

            Debug.Log("Load Success: " + name);
            
            OnLoaded?.Invoke();
            
            return true;
        }

        public bool Save()
        {
            if (!storable)
            {
                Debug.LogError(name + " Not Storable");
                
                return false;
            }
            
            if (!JsonStorageService<TModel>.Save(name, data))
                return false;
            
            Debug.Log("Save Success: " + name);

            OnSaved?.Invoke();

            return true;
        }

        public bool Delete()
        {
            if (!storable)
            {
                Debug.LogError(name + " Not Storable");
                
                return false;
            }
            
            if (!JsonStorageService<TModel>.Delete(name))
                return false;

            OnSavesDeleted?.Invoke();

            return true;
        }

        public object Clone()
        {
            return (TModel)data.Clone<TModel>();
        }

        public void Fill(object dataToFill)
        {
            data = (TModel)dataToFill;
        }
    }
}