using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Krem.JetPack.ProtoElements.Components
{
    [NodeGraphGroupName("Jet Pack/Proto Elements")]
    [DisallowMultipleComponent]
    public class RadialProgressBar : BaseProgressBar
    {
        [Header("Components")]
        [SerializeField, NotNull] protected Image _progressBarAmountImage;
        [SerializeField, NotNull] protected TMP_Text progressValueText;

        [Header("Settings")] 
        public float progressTextMultiplier = 100;

        public Image ProgressBarAmountImage => _progressBarAmountImage;

        private void Awake()
        {
            int newValue = Mathf.RoundToInt(ProgressValue * progressTextMultiplier);
            SetProgressText(newValue.ToString());
        }

        public void SetProgressText(string text)
        {
            progressValueText.text = text;
        }

        public override void SetProgressPosition(float value)
        {
            _progressBarAmountImage.fillAmount = value;
        }
    }
}