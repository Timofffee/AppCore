using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;
using Krem.JetPack.HyperControls.Components.Movables;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Movables
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Movables")]
    public class TransformMoveXZ : CoreAction
    {
        public InputData<float> DeltaTime;
        
        [InjectComponent] private TransformMovable _transformMovable;
        [InjectComponent] private TransformLink _transformComponent;

        private Vector3 _moveVector = Vector3.zero;
        
        protected override bool Action()
        {
            _moveVector.x = _transformMovable.InputAxis.Axis.x * _transformMovable.sensitivity * DeltaTime.Data;
            _moveVector.z = _transformMovable.InputAxis.Axis.y * _transformMovable.sensitivity * DeltaTime.Data;
            _moveVector.y = 0;
            
            _transformComponent.Transform.Translate(_moveVector);
        
            return true;
        }
    }
}