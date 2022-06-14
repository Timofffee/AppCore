using System;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.UI;

namespace Krem.JetPack.Basis.Components.Links
{
    [NodeGraphGroupName("Jet Pack/Basis/Links")]
    public class ButtonLink : CoreComponent
    {
        [Header("Dependencies")]
        [SerializeField, NotNull] protected Button _button;

        [Header("Ports")]
        public OutputSignal OnClick;

        public Button Button => _button;

        private void OnEnable()
        {
            Button.onClick.AddListener(ClickHandler);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(ClickHandler);
        }

        protected void ClickHandler()
        {
            OnClick.Invoke();
        }
    }
}