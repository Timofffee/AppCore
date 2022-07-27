using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Krem.AzureAnalytics.Actions.MaxSdkActions
{
    [NodeGraphGroupName("Azure Analytics/MaxSDK")]
    public class BindBannerEvents : CoreAction
    {
        [Header("Events")]
        public UnityEvent bannerLoaded;
        public UnityEvent bannerClicked;

        protected override bool Action()
        {
            UnbindEvents();
            BindEvents();

            return true;
        }

        protected void BindEvents()
        {
            MaxSdkComponent.onBannerLoaded.AddListener(Loaded);
            MaxSdkComponent.onBannerClicked.AddListener(Clicked);
        }
        
        protected void UnbindEvents()
        {
            MaxSdkComponent.onBannerLoaded.RemoveListener(Loaded);
            MaxSdkComponent.onBannerClicked.RemoveListener(Clicked);
        }

        protected void Loaded()
        {
            bannerLoaded.Invoke();
        }
        
        protected void Clicked()
        {
            bannerClicked.Invoke();
        }
    }
}