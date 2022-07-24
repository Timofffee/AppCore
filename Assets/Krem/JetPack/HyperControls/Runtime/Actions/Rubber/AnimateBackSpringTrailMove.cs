using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.HyperControls.Components.Rubber;
using UnityEngine;

namespace Krem.JetPack.HyperControls.Actions.Rubber
{
    [NodeGraphGroupName("Jet Pack/Hyper Controls/Rubber")] 
    public class AnimateBackSpringTrailMove : CoreAction
    {
        [ActionParameter] public float AnimationTime = 1f;
        
        public OutputSignal OnAnimateFinished;
        
        [InjectComponent] private RubberControl _rubberControl;

        private IEnumerator _coroutine;
        private float _elapsedTime = 0f;
        private float _offset;
        
        protected override bool Action()
        {
            if (_rubberControl.Triggered || !_rubberControl.gameObject.activeInHierarchy)
                return false;
            
            _rubberControl.Triggered = true;

            if (_coroutine != null)
            {
                _rubberControl.StopCoroutine(_coroutine);
            }
            
            _coroutine = Animate();
            _elapsedTime = 0f;
            _rubberControl.StartCoroutine(_coroutine);
            
            return true;
        }
        
        IEnumerator Animate()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                
                _elapsedTime += Time.deltaTime;

                _offset = Mathf.Lerp(
                    (_rubberControl.Handle.anchoredPosition - _rubberControl.Center.anchoredPosition).magnitude,
                    0f,
                    _elapsedTime / AnimationTime);
                _rubberControl.Trail.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _offset);

                if (_elapsedTime > AnimationTime)
                {
                    break;
                }
            }
            
            _rubberControl.Triggered = false;
            
            OnAnimateFinished.Invoke();
        }
    }
}