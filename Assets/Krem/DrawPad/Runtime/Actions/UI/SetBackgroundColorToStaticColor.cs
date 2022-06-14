using System.Collections;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.DrawPad.Actions.UI
{
    [NodeGraphGroupName("DrawPad/UI")]
    public class SetBackgroundColorToStaticColor : CoreAction
    {
        [InjectComponent] private Components.DrawPad _drawPad;

        [BindInputSignal(nameof(ResetTransition))]
        public InputSignal Reset;

        public OutputSignal OnTimeElapsed;

        [ActionParameter] public float TransitionTime = 0f;

        private float _elapsedTime = 0;
        private bool _triggered = false;
        private IEnumerator _coroutine;

        protected override bool Action()
        {
            _elapsedTime = 0;

            if (_triggered || !_drawPad.gameObject.activeInHierarchy)
                return false;

            _triggered = true;

            if (_coroutine != null)
            {
                _drawPad.StopCoroutine(_coroutine);
            }

            _coroutine = Transition();

            _drawPad.StartCoroutine(_coroutine);

            return true;
        }

        IEnumerator Transition()
        {
            while (_elapsedTime < TransitionTime)
            {
                yield return new WaitForEndOfFrame();
                _elapsedTime += Time.unscaledDeltaTime;
                Iterate();
            }

            _triggered = false;

            OnTimeElapsed.Invoke();
        }

        private void ResetTransition()
        {
            if (_coroutine != null)
            {
                _drawPad.StopCoroutine(_coroutine);
            }

            _triggered = false;
        }

        protected void Iterate()
        {
            Color color = Color.Lerp(
                _drawPad.Settings.Model.DrawBackgroundColor,
                _drawPad.Settings.Model.BackgroundColor,
                _elapsedTime / TransitionTime
            );

            _drawPad.BackGroundImage.color = color;
        }
    }
}