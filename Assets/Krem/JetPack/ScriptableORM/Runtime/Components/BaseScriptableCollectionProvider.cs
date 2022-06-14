using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Components
{
    public abstract class BaseScriptableCollectionProvider<TScriptableCollection, TModel> : CoreComponent where TModel : Model, new() where TScriptableCollection : ScriptableCollection<TModel>
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected TScriptableCollection _scriptableCollection;
        
        [Header("Ports")]
        [BindInputSignal(nameof(Save))] public InputSignal CallSave;
        [BindInputSignal(nameof(Load))] public InputSignal CallLoad;
        [BindInputSignal(nameof(Delete))] public InputSignal CallDelete;
        public OutputSignal OnSaved;
        public OutputSignal OnLoaded;
        public OutputSignal OnDelete;

        public TScriptableCollection ScriptableCollection => _scriptableCollection;
        public List<TModel> Collection
        {
            get => _scriptableCollection.Collection;
            set => _scriptableCollection.Collection = value;
        }

        private void OnEnable()
        {
            _scriptableCollection.OnLoaded += LoadHandler;
            _scriptableCollection.OnSaved += SaveHandler;
            _scriptableCollection.OnSavesDeleted += DeleteHandler;
        }

        private void OnDisable()
        {
            _scriptableCollection.OnLoaded -= LoadHandler;
            _scriptableCollection.OnSaved -= SaveHandler;
            _scriptableCollection.OnSavesDeleted -= DeleteHandler;
        }

        private void LoadHandler()
        {
            OnLoaded.Invoke();
        }

        private void SaveHandler()
        {
            OnSaved.Invoke();
        }

        private void DeleteHandler()
        {
            OnDelete.Invoke();
        }

        public void Save()
        {
            if (_scriptableCollection.Save() == false)
            {
                Debug.LogWarning("Cant Save: " + name);
            }
        }

        public void Load()
        {
            if (_scriptableCollection.Load() == false)
            {
                Debug.LogWarning("Cant Load: " + name);
            }
        }

        public void Delete()
        {
            if (_scriptableCollection.Delete() == false)
            {
                Debug.LogWarning("Cant Delete: " + name);
            }
        }
    }
}