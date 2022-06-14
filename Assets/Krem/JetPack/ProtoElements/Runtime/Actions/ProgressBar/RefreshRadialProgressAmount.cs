using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Actions.ProgressBar
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Progress Bar")]
    public class RefreshRadialProgressAmount : CoreAction
    {
        [InjectComponent] private Components.RadialProgressBar _progressBar;

        [BindInputSignal(nameof(ResetProgress))] public InputSignal Reset;

        private float _currentValue;
        private float _destinationValue;
        private float _currentDeltaTime = 0;
        private IEnumerator _currentCoroutine;

        protected override bool Action()
        {
            if (!_progressBar.gameObject.activeInHierarchy)
            {
                return false;
            }
            
            if (_progressBar.animateProgress)
            {
                if (_currentCoroutine != null)
                {
                    _progressBar.StopCoroutine(_currentCoroutine);
                }
                _currentCoroutine = Animate();
                _progressBar.StartCoroutine(_currentCoroutine);
            }
            else
            {
                _progressBar.SetProgressPosition(_progressBar.ProgressValue);
            }

            return true;
        }

        protected IEnumerator Animate()
        {
            _currentValue = _progressBar.PreviousValue;
            _destinationValue = _progressBar.ProgressValue;
            _currentDeltaTime = 0;
            
            while (Iterate())
            {
                yield return new WaitForEndOfFrame();
            }
        }

        protected bool Iterate()
        {
            if (IsActionTimeElapsed())
                _currentDeltaTime = _progressBar.animationTime;
            
            float time = _currentDeltaTime / _progressBar.animationTime;
            float interpolateValue = Mathf.Lerp(_currentValue, _destinationValue, time);
            
            _progressBar.SetProgressPosition(interpolateValue);

            if (IsActionTimeElapsed())
                return false;

            _currentDeltaTime += Time.unscaledDeltaTime;
            return true;
        }

        protected bool IsActionTimeElapsed()
        {
            if (_currentDeltaTime >= _progressBar.animationTime)
                return true;

            return false;
        }

        protected void ResetProgress()
        {
            if (_currentCoroutine != null)
            {
                _progressBar.StopCoroutine(_currentCoroutine);
            }
            
            _progressBar.SetProgressPosition(_progressBar.ProgressValue);
        }
    }
}