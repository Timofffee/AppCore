using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components;
using UnityEngine;

namespace Krem.JetPack.Basis.Actions
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class Delay : CoreAction
    {
        [InjectComponent] private CoroutineComponent _coroutineComponent;
        
        [ActionParameter] public float DelayTime = 0f;
        [ActionParameter] public float RandomRange = 0f;
        [ActionParameter] public bool UseUnscaledTime = false;
        
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
            if (UseUnscaledTime)
                yield return new WaitForSecondsRealtime(DelayTime + Random.Range(0f, RandomRange));
            else
                yield return new WaitForSeconds(DelayTime + Random.Range(0f, RandomRange));
            
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