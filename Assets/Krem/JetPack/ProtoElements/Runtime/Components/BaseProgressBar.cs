using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Components
{
    public abstract class BaseProgressBar : CoreComponent 
    {
        [Header("Data")]
        [Range(0f, 1f)]
        [SerializeField] private float progressValue = 0;

        [Header("Settings")] 
        public bool animateProgress = false;
        public float animationTime = .5f;

        [Header("Ports")]
        [BindInputSignal(nameof(ResetRequest))] public InputSignal CallResetRequest;
        public OutputSignal ValueChanged;
        public OutputSignal OnResetRequest;
        
        private float _previousValue = 0;

        public float ProgressValue
        {
            get => progressValue;
            set
            {
                _previousValue = progressValue;
                progressValue = Mathf.Clamp(value,0 ,1);
                
                ValueChanged.Invoke();
            }
        }
        public float PreviousValue => _previousValue;

        public abstract void SetProgressPosition(float value);

        public void Add(float value)
        {
            ProgressValue += value;
        }

        public void ResetRequest()
        {
            OnResetRequest.Invoke();
        }
    }
}