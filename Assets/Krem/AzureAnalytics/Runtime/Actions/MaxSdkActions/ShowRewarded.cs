using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Components;
using UnityEngine;

namespace Krem.AzureAnalytics.Actions.MaxSdkActions
{
    [NodeGraphGroupName("Azure Analytics/MaxSDK")]
    public class ShowRewarded : CoreAction
    {
        [Header("Events")]
        public OutputSignal OnRewardedAvailable;
        public OutputSignal OnRewardedNotReady;
        public OutputSignal OnRewardedDisplayed;
        public OutputSignal OnRewardedFailedToDisplay;
        public OutputSignal OnRewardedClicked;
        public OutputSignal OnRewardedDismissed;
        public OutputSignal OnRewardedReceiveReward;
        public OutputSignal OnRewardedHidden;

        protected override bool Action()
        {
            if (MaxSdk.IsRewardedAdReady(MaxSdkComponent.instance.rewardedAdUnitId))
            {
                BindEvents();
                
                OnRewardedAvailable.Invoke();
                
                MaxSdk.ShowRewardedAd(MaxSdkComponent.instance.rewardedAdUnitId);
            }
            else
            {
                OnRewardedNotReady.Invoke();
            }

            return true;
        }

        protected void BindEvents()
        {
            MaxSdkComponent.onRewardedAdFailedToDisplay.AddListener(FailedToDisplayEvent);
            MaxSdkComponent.onRewardedAdClicked.AddListener(Clicked);
            MaxSdkComponent.onRewardedAdDismissed.AddListener(Dismissed);
            MaxSdkComponent.onRewardedAdDisplayed.AddListener(Displayed);
            MaxSdkComponent.onRewardedAdReceivedReward.AddListener(Received);
            MaxSdkComponent.onRewardedAdHidden.AddListener(Hidden);
        }
        
        protected void UnbindEvents()
        {
            MaxSdkComponent.onRewardedAdFailedToDisplay.RemoveListener(FailedToDisplayEvent);
            MaxSdkComponent.onRewardedAdClicked.RemoveListener(Clicked);
            MaxSdkComponent.onRewardedAdDismissed.RemoveListener(Dismissed);
            MaxSdkComponent.onRewardedAdDisplayed.RemoveListener(Displayed);
            MaxSdkComponent.onRewardedAdReceivedReward.RemoveListener(Received);
            MaxSdkComponent.onRewardedAdHidden.RemoveListener(Hidden);
        }

        protected void FailedToDisplayEvent()
        {
            UnbindEvents();
            
            OnRewardedFailedToDisplay.Invoke();
        }
        
        protected void Clicked()
        {
            UnbindEvents();
            
            OnRewardedClicked.Invoke();
        }
        
        protected void Dismissed()
        {
            UnbindEvents();
            
            OnRewardedDismissed.Invoke();
        }
        
        protected void Displayed()
        {
            OnRewardedDisplayed.Invoke();
        }
        
        protected void Received()
        {
            UnbindEvents();
            
            OnRewardedReceiveReward.Invoke();
        }

        protected void Hidden()
        {
            OnRewardedHidden.Invoke();
        }
    }
}