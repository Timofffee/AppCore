using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.ProtoElements.Components;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Actions.ProgressBar
{
    [NodeGraphGroupName("Jet Pack/Proto Elements/Progress Bar")]
    public class RefreshHorizontalProgressAmount : CoreAction
    {
        [InjectComponent] private HorizontalProgressBar _horizontalProgressBar;

        [BindInputSignal(nameof(ResetProgress))] public InputSignal Reset;

        private float _currentValue;
        private float _destinationValue;
        private float _currentDeltaTime = 0;
        private IEnumerator _currentCoroutine;

        protected override bool Action()
        {
            if (!_horizontalProgressBar.gameObject.activeInHierarchy)
            {
                return false;
            }
            
            if (_horizontalProgressBar.animateProgress)
            {
                if (_currentCoroutine != null)
                {
                    _horizontalProgressBar.StopCoroutine(_currentCoroutine);
                }
                _currentCoroutine = Animate();
                _horizontalProgressBar.StartCoroutine(_currentCoroutine);
            }
            else
            {
                _horizontalProgressBar.SetProgressPosition(_horizontalProgressBar.ProgressValue);
            }

            return true;
        }

        protected IEnumerator Animate()
        {
            _currentValue = _horizontalProgressBar.PreviousValue;
            _destinationValue = _horizontalProgressBar.ProgressValue;
            _currentDeltaTime = 0;
            
            while (Iterate())
            {
                yield return new WaitForEndOfFrame();
            }
        }

        protected bool Iterate()
        {
            if (IsActionTimeElapsed())
                _currentDeltaTime = _horizontalProgressBar.animationTime;
            
            float time = _currentDeltaTime / _horizontalProgressBar.animationTime;
            float interpolateValue = Mathf.Lerp(_currentValue, _destinationValue, time);
            
            _horizontalProgressBar.SetProgressPosition(interpolateValue);

            if (IsActionTimeElapsed())
                return false;

            _currentDeltaTime += Time.unscaledDeltaTime;
            return true;
        }

        protected bool IsActionTimeElapsed()
        {
            if (_currentDeltaTime >= _horizontalProgressBar.animationTime)
                return true;

            return false;
        }

        protected void ResetProgress()
        {
            if (_currentCoroutine != null)
            {
                _horizontalProgressBar.StopCoroutine(_currentCoroutine);
            }
            
            _horizontalProgressBar.SetProgressPosition(_horizontalProgressBar.ProgressValue);
        }
    }
}