using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DragMergeMatch.Models;
using UnityEngine;

namespace Krem.DragMergeMatch.Components
{
    [NodeGraphGroupName("Drag Merge Match")]
    [DisallowMultipleComponent]
    public class PlaceholdersStatesProvider : CoreComponent
    {    
        [Header("Dependencies")]
        public PlaceholderStateModelScriptableList _placeholderStateModelScriptableList;

        [Header("Ports")]
        [BindInputSignal(nameof(Load))] public InputSignal CallLoad;
        [BindInputSignal(nameof(Save))] public InputSignal CallSave;
        
        public OutputSignal OnLoad;
        public OutputSignal OnSave;

        private void Awake()
        {
            if (_placeholderStateModelScriptableList == null)
            {
                throw new ArgumentNullException(nameof(_placeholderStateModelScriptableList));
            }
        }

        public void Load()
        {
            _placeholderStateModelScriptableList.Load();
            
            OnLoad.Invoke();
        }
        
        public void Save()
        {
            if (_placeholderStateModelScriptableList.Save())
            {
                OnSave.Invoke();
            }
        }
    }
}