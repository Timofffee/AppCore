using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions.Transform
{
    [NodeGraphGroupName("Jet Pack/Basis/Transform")]
    public class TransformDirectionMove : CoreAction
    {
        public InputData<float> DeltaTime;

        [InjectComponent] private UnityEngine.Transform _transform;

        [ActionParameter] public Vector3 MoveSpeed = Vector3.zero;
        [ActionParameter] public bool LocalSpace = false;

        protected override bool Action()
        {
            _transform.Translate(MoveSpeed * DeltaTime.Data, LocalSpace ? Space.Self : Space.World);

            return true;
        }
    }
}