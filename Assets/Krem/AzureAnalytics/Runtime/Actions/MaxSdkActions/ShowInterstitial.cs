using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Components;
using UnityEngine;

namespace Krem.AzureAnalytics.Actions.MaxSdkActions
{
    [NodeGraphGroupName("Azure Analytics/MaxSDK")]
    public class ShowInterstitial : CoreAction
    {
        [Header("Ports")]
        public OutputSignal OnInterstitialAvailable;
        public OutputSignal OnInterstitialNotReady;
        public OutputSignal OnInterstitialDisplayed;
        public OutputSignal OnInterstitialFailedToDisplay;
        public OutputSignal OnInterstitialClicked;
        public OutputSignal OnInterstitialHidden;

        protected override bool Action()
        {
            if (MaxSdk.IsInterstitialReady(MaxSdkComponent.instance.interstitialAdUnitId))
            {
                BindEvents();
                
                OnInterstitialAvailable.Invoke();
                
                MaxSdk.ShowInterstitial(MaxSdkComponent.instance.interstitialAdUnitId);
            }
            else
            {
                OnInterstitialNotReady.Invoke();
            }

            return true;
        }

        protected void BindEvents()
        {
            MaxSdkComponent.onInterstitialFailedToDisplay.AddListener(FailedToDisplayEvent);
            MaxSdkComponent.onInterstitialClicked.AddListener(Clicked);
            MaxSdkComponent.onInterstitialHidden.AddListener(Hidden);
            MaxSdkComponent.onInterstitialDisplayed.AddListener(Displayed);
        }
        
        protected void UnbindEvents()
        {
            MaxSdkComponent.onInterstitialFailedToDisplay.RemoveListener(FailedToDisplayEvent);
            MaxSdkComponent.onInterstitialClicked.RemoveListener(Clicked);
            MaxSdkComponent.onInterstitialHidden.RemoveListener(Hidden);
            MaxSdkComponent.onInterstitialDisplayed.RemoveListener(Displayed);
        }

        protected void FailedToDisplayEvent()
        {
            UnbindEvents();
            
            OnInterstitialFailedToDisplay.Invoke();
        }
        
        protected void Clicked()
        {
            UnbindEvents();
            
            OnInterstitialClicked.Invoke();
        }
        
        protected void Hidden()
        {
            UnbindEvents();
            
            OnInterstitialHidden.Invoke();
        }
        
        protected void Displayed()
        {
            UnbindEvents();
            
            OnInterstitialDisplayed.Invoke();
        }
    }
}