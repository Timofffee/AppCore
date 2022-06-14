using System;
using System.Collections.Generic;
using Krem.JetPack.ScriptableORM.Extensions;
using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM
{
    public abstract class ScriptableCollection<TModel> : ScriptableObject, IScriptableRepository where TModel : Model, new()
    {
        [Header("Settings")]
        [SerializeField] protected bool storable = true;
        [SerializeField] protected bool immutable = false;
        [SerializeField] protected bool dataProtected = false;
        
        [Header("Data")]
        [SerializeField] protected List<TModel> data = new List<TModel>();

        public event Action OnLoaded;
        public event Action OnSaved;
        public event Action OnSavesDeleted;
        public event Action OnSetData;
        
        public List<TModel> Collection
        {
            get
            {
                if (immutable)
                {
                    return (List<TModel>)Clone();
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
                
                data = (List<TModel>) (immutable ? value.Clone<TModel>() : value);
                
                OnDataIsSet();
                OnSetData?.Invoke();
            }
        }

        private void OnValidate()
        {
            data.ForEach(item =>
            {
                if (string.IsNullOrEmpty(item.Guid))
                {
                    item.RegenerateGuid();
                }
            });
        }

        protected virtual void OnLoad() { }
        protected virtual void OnSave() { }
        protected virtual void OnDeleteSaves() { }
        protected virtual void OnDataIsSet() { }

        public bool IsStorable()
        {
            return storable;
        }
        
        public bool Load()
        {
            if (!storable)
            {
                Debug.LogError(name + " Not Storable");
                
                return false;
            }
            
            if (!JsonStorageService<TModel>.Load(name, out List<TModel> loadedData))
                return false;

            data = loadedData;
            
            Debug.Log("Load Success: " + name);
            
            OnLoad();
            
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
            
            OnSave();
            
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

            OnDeleteSaves();
            OnSavesDeleted?.Invoke();

            return true;
        }
        
        public object Clone()
        {
            return (List<TModel>)data.Clone<TModel>();
        }

        public void Fill(object dataToFill)
        {
            Collection = (List<TModel>)dataToFill;
        }

        public void Clear()
        {
            if (dataProtected)
            {
                Debug.LogError("Try to Clear data when data protected " + name);
                    
                return;
            }
            
            data.Clear();
        }

        public TModel FindByGUID(string guid)
        {
            return Collection.Find(item => item.Guid == guid);
        }
    }
}