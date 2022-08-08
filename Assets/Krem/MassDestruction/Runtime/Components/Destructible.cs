using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.MassDestruction.Interfaces;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent] 
    public class Destructible : CoreComponent, IDestructible
    {
        [Header("Components")]
        public Solid solid;
        public Fractured fractured;

        [Header("States")]
        [SerializeField] private bool _destructed = false;

        
        [BindInputSignal(nameof(Destruct))] public InputSignal CallDestruct;
        public OutputSignal Destructed;
        
        public bool DestructedState
        {
            get => _destructed;
            set
            {
                if (_destructed)
                    return;

                _destructed = value;

                if (_destructed)
                {
                    Destructed.Invoke();
                }
            }
        }

        private void Awake()
        {
            if (solid == null)
            {
                throw new Exception("solid is not set");
            }
            
            if (fractured == null)
            {
                throw new Exception("fractured is not set");
            }
            
            fractured.Init();
            fractured.DoDisable();
            solid.Destructible = this;
        }

        public void Destruct()
        {
            DestructedState = true;
        }
    }
}