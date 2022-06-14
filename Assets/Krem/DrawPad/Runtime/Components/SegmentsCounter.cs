using System;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using TMPro;
using UnityEngine;

namespace Krem.DrawPad.Components
{
    [NodeGraphGroupName("DrawPad")]
    [DisallowMultipleComponent]
    public class SegmentsCounter : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] private DrawPad _drawPad;
        [SerializeField, NotNull] private TMP_Text _countText;

        [Header("Ports")]
        public OutputSignal OnDraw;

        public DrawPad DrawPad => _drawPad;
        public TMP_Text CountText => _countText;

        private void OnEnable()
        {
            DrawPad.OnDrawing.AddListener(DrawHandler);
            DrawPad.OnEndDrawing.AddListener(DrawHandler);
        }

        private void OnDisable()
        {
            DrawPad.OnDrawing.RemoveListener(DrawHandler);
            DrawPad.OnEndDrawing.RemoveListener(DrawHandler);
        }

        private void DrawHandler()
        {
            OnDraw.Invoke();
        }
    }
}