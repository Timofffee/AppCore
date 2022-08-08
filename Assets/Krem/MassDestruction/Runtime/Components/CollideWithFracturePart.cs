using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.MassDestruction.Components
{
    [NodeGraphGroupName("Mass Destruction")]
    [DisallowMultipleComponent] 
    public class CollideWithFracturePart : CoreComponent
    {
        [Header("Ports")]
        public OutputSignal OnCollision;

        private FracturedPart _fracturedPart;
        public FracturedPart CollidedFracturePart => _fracturedPart;


        private void OnCollisionEnter(Collision other)
        {
            FracturedPart fracturedPartComponent = other.gameObject.GetComponent<FracturedPart>();
            if (fracturedPartComponent != null)
            {
                _fracturedPart = fracturedPartComponent;
                
                OnCollision.Invoke();
            }
        }
    }
}