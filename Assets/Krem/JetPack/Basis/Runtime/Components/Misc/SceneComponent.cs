using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.JetPack.Basis.Components
{
    [NodeGraphGroupName("Jet Pack/Basis/Misc")]
    public class SceneComponent : CoreComponent
    {    
        [Header("Data")]
        public string sceneName;
        
        [Header("Settings")]
        public bool reloadCurrentScene = false;

        [Header("Ports")]
        public OutputSignal RequestLoading;

        public void RequestLoadScene()
        {
            RequestLoading.Invoke();
        }
    }
}