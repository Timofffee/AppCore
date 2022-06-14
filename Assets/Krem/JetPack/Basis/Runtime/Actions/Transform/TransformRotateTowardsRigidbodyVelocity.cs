using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions.Transform
{
    [NodeGraphGroupName("Jet Pack/Basis/Transform")]
    public class TransformRotateTowardsRigidbodyVelocity : CoreAction
    {
        [InjectComponent] private UnityEngine.Transform _transform;
        [InjectComponent] private UnityEngine.Rigidbody _rigidbody;
        
        [ActionParameter] public float ActivationThreshold = 0.09f;
        [ActionParameter] public bool X = false;
        [ActionParameter] public bool Y = false;
        [ActionParameter] public bool Z = false;

        private Vector3 _actualDirection = Vector3.zero;

        protected override bool Action()
        {
            _actualDirection = _rigidbody.velocity;

            if (X) _actualDirection.x = 0;
            if (Y) _actualDirection.y = 0;
            if (Z) _actualDirection.z = 0;

            if (_actualDirection.magnitude < ActivationThreshold)
                return false;
            
            _transform.rotation = Quaternion.LookRotation(_actualDirection);

            return true;
        }
    }
}