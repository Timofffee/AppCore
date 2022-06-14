using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;

namespace Krem.JetPack.Basis.Actions.Transform
{
    [NodeGraphGroupName("Jet Pack/Basis/Transform")]
    public class TransformRotateFaceToMainCamera : CoreAction
    {
        public InputComponent<CameraLink> CameraLink;
        
        [InjectComponent] private UnityEngine.Transform _transform;
        
        [ActionParameter] public float offsetX = 0f;
        [ActionParameter] public float offsetY = 0f;
        [ActionParameter] public float offsetZ = 0f;
        [ActionParameter] public bool fixedHorizon = false;

        protected override bool Action()
        {
            if (fixedHorizon)
            {
                _transform.rotation = CameraLink.Component.Camera.transform.rotation;
            }
            else
            {
                _transform.LookAt(CameraLink.Component.Camera.transform);
            }
            
            _transform.Rotate(offsetX, offsetY, offsetZ);

            return true;
        }
    }
}