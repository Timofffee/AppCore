using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class WaitForNextFrameDelay : CoreAction
    {
        [InjectComponent] private CoroutineComponent _coroutineComponent;
        
        [BindInputSignal(nameof(ResetDelay))] public InputSignal Reset;
        public OutputSignal OnTimeElapsed;

        private bool _triggered = false;
        private IEnumerator _coroutine;

        protected override bool Action()
        {
            if (_triggered || !_coroutineComponent.gameObject.activeInHierarchy)
                return false;
            
            _triggered = true;

            if (_coroutine != null)
            {
                _coroutineComponent.StopCoroutine(_coroutine);
            }
            _coroutine = DelayInSeconds();
            
            _coroutineComponent.StartCoroutine(_coroutine);
            
            return true;
        }

        IEnumerator DelayInSeconds()
        {
            yield return new WaitForEndOfFrame();
            
            _triggered = false;
            
            OnTimeElapsed.Invoke();
        }

        private void ResetDelay()
        {
            if (_coroutine != null)
            {
                _coroutineComponent.StopCoroutine(_coroutine);
            }

            _triggered = false;
        }
    }
}