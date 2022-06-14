using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Components
{
    public abstract class BaseScriptableModelProvider<TScriptableModel, TModel> : CoreComponent where TModel : Model, new() where TScriptableModel : ScriptableModel<TModel>
    {    
        [Header("Dependencies")]
        [SerializeField, NotNull] protected TScriptableModel _scriptableModel;
        
        [Header("Ports")]
        [BindInputSignal(nameof(Save))] public InputSignal CallSave;
        [BindInputSignal(nameof(Load))] public InputSignal CallLoad;
        [BindInputSignal(nameof(Delete))] public InputSignal CallDelete;
        public OutputSignal OnSaved;
        public OutputSignal OnLoaded;
        public OutputSignal OnDelete;
        public OutputSignal OnValueChanged;

        public TModel Model => _scriptableModel.Model;
        
        private void OnEnable()
        {
            _scriptableModel.OnLoaded += LoadHandler;
            _scriptableModel.OnSaved += SaveHandler;
            _scriptableModel.OnSavesDeleted += DeleteHandler;
            _scriptableModel.Model.PropertyChanged += ValueChangedHandler;
        }

        private void OnDisable()
        {
            _scriptableModel.OnLoaded -= LoadHandler;
            _scriptableModel.OnSaved -= SaveHandler;
            _scriptableModel.OnSavesDeleted -= DeleteHandler;
            _scriptableModel.Model.PropertyChanged -= ValueChangedHandler;
        }

        private void ValueChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            OnValueChanged.Invoke();
        }

        private void LoadHandler()
        {
            _scriptableModel.Model.PropertyChanged += ValueChangedHandler;
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
            if (_scriptableModel.Save() == false)
            {
                Debug.LogWarning("Cant Save: " + name);
            }
        }

        public void Load()
        {
            _scriptableModel.Model.PropertyChanged -= ValueChangedHandler;
            
            if (_scriptableModel.Load() == false)
            {
                Debug.LogWarning("Cant Load: " + name);
            }
        }
        
        public void Delete()
        {
            if (_scriptableModel.Delete() == false)
            {
                Debug.LogWarning("Cant Delete: " + name);
            }
        }
    }
}