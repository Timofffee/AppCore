using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class Button : CoreComponent
    {    
        [Header("Ports")]
        [BindInputSignal(nameof(Click))] public InputSignal CallClick;
        public OutputSignal OnClick;

        public void Click()
        {
            OnClick.Invoke();
        }
    }
}