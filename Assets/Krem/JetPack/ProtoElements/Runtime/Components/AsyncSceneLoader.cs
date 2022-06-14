using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.JetPack.Basis.Components;
using UnityEngine;

namespace Krem.JetPack.ProtoElements.Components
{
    [NodeGraphGroupName("Jet Pack/Proto Elements")]
    [DisallowMultipleComponent]
    public class AsyncSceneLoader : CoreComponent
    {    
        [Header("Dependencies")]
        [SerializeField] private SceneComponent _sceneComponent;
        [SerializeField] private HorizontalProgressBar horizontalProgressBar;

        [Header("Ports")]
        [BindInputSignal(nameof(LoadRequest))] public InputSignal CallLoadRequest;
        public OutputSignal OnLoadRequest;

        public SceneComponent SceneComponent => _sceneComponent;
        public HorizontalProgressBar HorizontalProgressBar => horizontalProgressBar;

        public void LoadRequest()
        {
            gameObject.SetActive(true);
            horizontalProgressBar.ProgressValue = 0;
            
            OnLoadRequest.Invoke();
        }
    }
}