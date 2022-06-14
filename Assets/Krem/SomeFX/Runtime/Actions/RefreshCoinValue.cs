using System;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using TMPro;
using UnityEngine;

namespace Krem.SomeFX.Actions
{
    [NodeGraphGroupName("Some FX/Timer")]
    public class RefreshCoinValue : CoreAction
    {
        [Header("Data")]
        public int value;
        [SerializeField, NotNull] private TMP_Text text;

        [Header("Settings")]
        public float actionTime = 1f;
        
        private int _currentCoinValue;
        private int _destinationCoinValue;
        private float _currentDeltaTime = 0;

        protected bool Iterate()
        {
            if (IsActionTimeElapsed())
                _currentDeltaTime = actionTime;
            
            float time = _currentDeltaTime / actionTime;
            float interpolateCoinsValue = Mathf.Lerp(_currentCoinValue, _destinationCoinValue, time);
            text.text = Mathf.RoundToInt(interpolateCoinsValue).ToString();

            if (IsActionTimeElapsed())
                return false;

            _currentDeltaTime += Time.unscaledDeltaTime;
            return true;
        }

        public void StartAction()
        {
            _currentCoinValue = Convert.ToInt32(text.text);
            _destinationCoinValue = value;
            
            _currentDeltaTime = 0;

            if (_currentCoinValue == _destinationCoinValue)
                return;
        }

        public void StartAction(int newValue)
        {
            value = newValue;
            
            StartAction();
        }
        
        private bool IsActionTimeElapsed()
        {
            if (_currentDeltaTime >= actionTime)
                return true;

            return false;
        }

        protected override bool Action()
        {
            throw new NotImplementedException();
        }
    }
}
