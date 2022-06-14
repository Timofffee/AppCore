using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.SomeFX.Components;
using UnityEngine;

namespace Krem.SomeFX.Actions.Materials
{
    [NodeGraphGroupName("Some FX")]
    public class TimedAnimateDissolveMaterial : CoreAction
    {
        [Header("Attributes")]
        public DissolveMaterial revealedItemAttribute;

        [Header("Settings")]
        public float animationTime = 1f;
        public float startValue = 0f;
        public float endValue = 1f;

        private float _elapsedTime = 0f;
        private static readonly int Dissolve = Shader.PropertyToID("Vector1_EE6959DF");
        private Material _targetMaterial;

        protected bool Iterate()
        {
            _elapsedTime += Time.deltaTime;
            float animationValue = Mathf.Lerp(startValue, endValue,  _elapsedTime / animationTime);
            _targetMaterial.SetFloat(Dissolve, animationValue);
            
            if (_elapsedTime >= animationTime)
            {
                return false;
            }
            
            return true;
        }

        public  void StartAction()
        {
            _targetMaterial = revealedItemAttribute.ItemMesh.sharedMaterial;
            _targetMaterial.SetFloat(Dissolve, startValue);
            _elapsedTime = 0f;
        }

        //TODO: Refactor this
        protected override bool Action()
        {
            throw new System.NotImplementedException();
        }
    }
}