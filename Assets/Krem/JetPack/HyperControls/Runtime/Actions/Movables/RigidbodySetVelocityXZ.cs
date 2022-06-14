using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.JetPack.HyperControls.Components.Movables;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Movables
{
    [NodeGraphGroupName("Jet Pack/HyperControls/Movables")] 
    public class RigidbodySetVelocityXZ : CoreAction
    {
        [InjectComponent] private RigidbodyMovable _rigidbodyMovable;
        
        private Vector3 _moveVector = Vector3.zero;

        protected override bool Action()
        {
            _moveVector.x = _rigidbodyMovable.InputAxis.Axis.x * _rigidbodyMovable.sensitivity;
            _moveVector.z = _rigidbodyMovable.InputAxis.Axis.y * _rigidbodyMovable.sensitivity;
            _moveVector.y = 0;
            
            _rigidbodyMovable.Rigidbody.velocity = _moveVector;
        
            return true;
        }
    }
}