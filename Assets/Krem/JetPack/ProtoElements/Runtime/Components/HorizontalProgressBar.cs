using System.Diagnostics.CodeAnalysis;
using Krem.AppCore.Attributes;
using TMPro;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Components
{
    [NodeGraphGroupName("Jet Pack/Proto Elements")]
    [DisallowMultipleComponent]
    public class HorizontalProgressBar : BaseProgressBar
    {
        [Header("Components")]
        [SerializeField, NotNull] protected RectTransform _progressBarAmountTransform;
        [SerializeField, NotNull] protected TMP_Text progressValueText;

        [Header("Settings")] 
        public float progressTextMultiplier = 100;

        private float _initialWidth;
        private Vector2 _initialOffsetMax;

        public RectTransform ProgressBarAmountTransform => _progressBarAmountTransform;
        public float InitialWidth => _initialWidth;
        public Vector2 InitialOffsetMax => _initialOffsetMax;

        private void Awake()
        {
            _initialOffsetMax = ProgressBarAmountTransform.offsetMax;
            _initialWidth = ProgressBarAmountTransform.rect.width;

            SetProgressPosition(ProgressValue);

            int newValue = Mathf.RoundToInt(ProgressValue * progressTextMultiplier);
            SetProgressText(newValue.ToString());
        }

        public override void SetProgressPosition(float value)
        {
            float width = value * InitialWidth - InitialWidth + InitialOffsetMax.x;
            ProgressBarAmountTransform.offsetMax = new Vector2(
                width,
                InitialOffsetMax.y);
        }

        public void SetProgressText(string text)
        {
            progressValueText.text = text;
        }
    }
}