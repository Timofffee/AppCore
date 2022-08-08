using System.Collections.Generic;
using System.Linq;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent] 
    public class Fractured : CoreComponent
    {
        [Header("Ports")]
        [BindInputSignal(nameof(DoDisable))] public InputSignal CallDoDisable;
        [BindInputSignal(nameof(DoEnable))] public InputSignal CallDoEnable;
        
        private List<Rigidbody> _rigidbodies;
        
        public List<Rigidbody> Rigidbodies => _rigidbodies;
        public Transform Transform { get; private set; }
        
        private void Awake()
        {
            Init();
        }
        
        public void DoDisable()
        {
            gameObject.SetActive(false);
        }

        public void DoEnable()
        {
            gameObject.SetActive(true);
        }

        public void Init()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
            Transform = transform;
        }
    }
}