using App.CubeMatch.Components.Controls;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components.Links;
using UnityEngine;

namespace App.CubeMatch.Actions.Controls
{
    [NodeGraphGroupName("Cube Match/Controls")] 
    public class TouchRotate : CoreAction
    {
        [ActionParameter] public float Sensetivity = .4f;
        
        public InputComponent<TouchControllerProvider> TouchControllerProvider;
        public InputComponent<TransformLink> TransformLink;

        private Vector3 _angle = Vector3.zero;
        
        protected override bool Action()
        {
            _angle.x = TouchControllerProvider.Component.TouchController.PointerEventData.delta.y * Sensetivity;
            _angle.y = -TouchControllerProvider.Component.TouchController.PointerEventData.delta.x * Sensetivity;
            TransformLink.Component.Transform.Rotate(_angle, Space.World);
        
            return true;
        }
    }
}