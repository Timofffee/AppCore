using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM
{
    [System.Serializable]
    public class Model : IHaveGuid, INotifyPropertyChanged
    {
        [SerializeField] protected string _name;
        [SerializeField] protected string _guid;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Name { get => _name; set => SetField(ref _name, value); }
        public string Guid
        {
            get => _guid;
            private set => _guid = value;
        }

        public Model()
        {
            _guid = System.Guid.NewGuid().ToString();
        }

        public Model Clone<TModel>() where TModel : Model, new()
        {
            string json = JsonUtility.ToJson(this);
            Model clone = new TModel();
            JsonUtility.FromJsonOverwrite(json, clone);

            return clone;
        }

        public void RegenerateGuid()
        {
            _guid = System.Guid.NewGuid().ToString();
            Debug.Log("Model Guid Regenerated");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value,[CallerMemberName] string propertyName =  null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            
            field = value;
            
            OnPropertyChanged(propertyName);
            
            return true;
        }
    }
}