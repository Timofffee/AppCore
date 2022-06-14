using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.CoinCollector.Components
{
    [NodeGraphGroupName("CoinCollector")]
    [DisallowMultipleComponent]
    public class CoinCollector : CoreComponent
    {    
        [Header("Ports")] 
        public OutputSignal OnCollect;

        public void Collect()
        {
            OnCollect.Invoke();
        }
    }
}