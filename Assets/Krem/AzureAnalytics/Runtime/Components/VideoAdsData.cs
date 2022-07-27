using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Types;
using UnityEngine;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class VideoAdsData : CoreComponent 
    {    
        [Header("Data")]
        public AdType adType;
        public AdPlacement adPlacement;

        [Header("Ports")]
        [BindInputSignal(nameof(ShowRequest))] public InputSignal CallShowRequest;
        public OutputSignal OnShowRequest;
        public OutputSignal OnMaxSdkNotInitialized;

        public void ShowRequest()
        {
            if (MaxSdkComponent.instance == null || MaxSdk.IsInitialized() == false)
            {
                Debug.LogWarning( "On Ads Show Request MaxSdkNotInitialized");
                OnMaxSdkNotInitialized.Invoke();
                
                return;
            }
            
            OnShowRequest.Invoke();
        }
    }
}