using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent] 
    public class CollideWithSolid : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnCollision;

        private Solid _solid;

        public Solid CollidedSolid => _solid;

        private void OnCollisionEnter(Collision other)
        {
            Solid solidComponent = other.gameObject.GetComponent<Solid>();
            
            if (solidComponent == null)
                return;
            
            _solid = solidComponent;

            OnCollision.Invoke();
        }
    }
}