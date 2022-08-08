using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [RequireComponent(typeof(Rigidbody))]
    [DisallowMultipleComponent] 
    public class Solid : CoreComponent
    {
        [HideInInspector] [BindInputSignal(nameof(DoDisable))] public InputSignal Disable;
        [HideInInspector] [BindInputSignal(nameof(DoEnable))] public InputSignal Enable;
        
        public Destructible Destructible { get; set; }
        public Transform Transform { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Transform = transform;
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void DoDisable()
        {
            gameObject.SetActive(false);
        }

        public void DoEnable()
        {
            gameObject.SetActive(true);
        }
    }
}