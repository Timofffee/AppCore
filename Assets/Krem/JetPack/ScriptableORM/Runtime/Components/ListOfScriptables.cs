using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ScriptableORM.Interfaces;
using UnityEngine;

namespace Krem.JetPack.ScriptableORM.Components
{
    [NodeGraphGroupName("Jet Pack/Scriptable ORM")]
    public class ListOfScriptables : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField] private List<ScriptableObject> repositories = new List<ScriptableObject>();
        
        [Header("Ports")]
        [BindInputSignal(nameof(Save))] public InputSignal CallSave;
        [BindInputSignal(nameof(Load))] public InputSignal CallLoad;
        public OutputSignal OnSaved;
        public OutputSignal OnLoaded;
        
        public void Save()
        {
            repositories.ForEach(repository =>
            {
                (repository as IScriptableRepository)?.Save();
            });
            
            
            OnSaved.Invoke();
        }

        public void Load()
        {
            repositories.ForEach(repository =>
            {
                (repository as IScriptableRepository)?.Load();
            });

            OnLoaded.Invoke();
        }
    }
}