using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;
using UnityEngine.Events;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class MaxSdkComponent : CoreComponent
    {
        [Header("Settings")]
        public string maxSdkKey;
        public bool reportInDebugLog = false;
        
        [Header("Interstitial")]
        public string interstitialAdUnitId;
        public bool interstitial = true; 
        
        [Header("Rewarded")]
        public string rewardedAdUnitId;
        public bool reward = true;
        
        [Header("Rewarded Interstitial")]
        public string rewardedInterstitialAdUnitId;
        public bool rewardedInterstitial = true;
        
        [Header("Banner")]
        public string bannerAdUnitId;
        public MaxSdkBase.BannerPosition bannerPosition = MaxSdkBase.BannerPosition.BottomCenter;
        public bool banner = true;
        
        [Header("M Rec")]
        public string mRecAdUnitId;
        public bool mRec = true;

        [Header("Events")]
        public UnityEvent onInitialized;

        [Header("Ports")]
        [BindInputSignal(nameof(Initialize))] public InputSignal CallInitialize;
        public OutputSignal OnInitialize;

        public static MaxSdkComponent instance;

        #region Static Events Binds

        // Interstitial
        public static readonly UnityEvent onInterstitialLoaded = new UnityEvent();
        public static readonly UnityEvent onInterstitialFailed = new UnityEvent();
        public static readonly UnityEvent onInterstitialFailedToDisplay = new UnityEvent();
        public static readonly UnityEvent onInterstitialClicked = new UnityEvent();
        public static readonly UnityEvent onInterstitialHidden = new UnityEvent();
        public static readonly UnityEvent onInterstitialDisplayed = new UnityEvent();

        // Rewarded
        public static readonly UnityEvent onRewardedAdLoaded = new UnityEvent();
        public static readonly UnityEvent onRewardedAdFailed = new UnityEvent();
        public static readonly UnityEvent onRewardedAdFailedToDisplay = new UnityEvent();
        public static readonly UnityEvent onRewardedAdClicked = new UnityEvent();
        public static readonly UnityEvent onRewardedAdDismissed = new UnityEvent();
        public static readonly UnityEvent onRewardedAdDisplayed = new UnityEvent();
        public static readonly UnityEvent onRewardedAdReceivedReward = new UnityEvent();
        public static readonly UnityEvent onRewardedAdHidden = new UnityEvent();

        // Banner
        public static readonly UnityEvent onBannerLoaded = new UnityEvent();
        public static readonly UnityEvent onBannerClicked = new UnityEvent();
        
        #endregion
        

        private bool _isMRecShowing;
        private int _interstitialRetryAttempt;
        private int _rewardedRetryAttempt;
        private int _rewardedInterstitialRetryAttempt;

        public void Initialize()
        {
            instance = this;
            
            ClearInterstitialStaticEvents();
            ClearRewardedStaticEvents();
            ClearBannerStaticEvents();
            
            if (MaxSdk.IsInitialized())
            {
                OnInitialize.Invoke();
                onInitialized.Invoke();
                
                return;
            }

            MaxSdk.SetSdkKey(maxSdkKey);
            MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration => Initialize(sdkConfiguration);
            MaxSdk.InitializeSdk();

            if (reportInDebugLog)
                Debug.Log("MaxSdkSystem Configured");
        }

        private void ClearInterstitialStaticEvents()
        {
            onInterstitialLoaded.RemoveAllListeners();
            onInterstitialFailed.RemoveAllListeners();
            onInterstitialFailedToDisplay.RemoveAllListeners();
            onInterstitialHidden.RemoveAllListeners();
            onInterstitialDisplayed.RemoveAllListeners();
        }
        
        private void ClearRewardedStaticEvents()
        {
            onRewardedAdLoaded.RemoveAllListeners();
            onRewardedAdFailed.RemoveAllListeners();
            onRewardedAdFailedToDisplay.RemoveAllListeners();
            onRewardedAdDisplayed.RemoveAllListeners();
            onRewardedAdClicked.RemoveAllListeners();
            onRewardedAdDismissed.RemoveAllListeners();
            onRewardedAdReceivedReward.RemoveAllListeners();
        }
        
        private void ClearBannerStaticEvents()
        {
            onBannerLoaded.RemoveAllListeners();
            onBannerClicked.RemoveAllListeners();
        }

        private void Initialize(MaxSdkBase.SdkConfiguration sdkConfiguration)
        {
            if (!String.IsNullOrEmpty(interstitialAdUnitId) & interstitial)
                InitializeInterstitialAds();
                
            if (!String.IsNullOrEmpty(rewardedAdUnitId) & reward)
                InitializeRewardedAds();

            if (!String.IsNullOrEmpty(rewardedInterstitialAdUnitId) & rewardedInterstitial)
                InitializeRewardedInterstitialAds();

            if (!String.IsNullOrEmpty(bannerAdUnitId) & banner)
            {
                InitializeBannerAds();
                MaxSdk.ShowBanner(bannerAdUnitId);
            }

            if (!String.IsNullOrEmpty(mRecAdUnitId) & mRec)
                InitializeMRecAds();

            if (reportInDebugLog)
                Debug.Log("Max SDK Initialized");
            
            onInitialized.Invoke();
            OnInitialize.Invoke();
        }
        
        #region Interstitial

        private void InitializeInterstitialAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;

            // Load the first interstitial
            MaxSdk.LoadInterstitial(interstitialAdUnitId);

            if (reportInDebugLog)
                Debug.Log("Initialize Interstitial Ads");
        }

        private void OnInterstitialLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
            
            // Reset retry attempt
            _interstitialRetryAttempt = 0;
            
            onInterstitialLoaded.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Interstitial loaded");
        }

        private void OnInterstitialFailedEvent(string arg1, MaxSdkBase.ErrorInfo arg2)
        {
            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            _interstitialRetryAttempt++;
            
            double retryDelay = Math.Pow(2, Math.Min(6, _interstitialRetryAttempt));

            void LoadInterstitialAd() => MaxSdk.LoadInterstitial(interstitialAdUnitId);

            Invoke(nameof(LoadInterstitialAd), (float) retryDelay);
            
            onInterstitialFailed.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Interstitial failed to load with error: " + arg2.Message);
        }

        private void OnInterstitialFailedToDisplayEvent(string arg1, MaxSdkBase.ErrorInfo arg2, MaxSdkBase.AdInfo arg3)
        {
            // Interstitial ad failed to display. We recommend loading the next ad
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
            
            onInterstitialFailedToDisplay.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Interstitial failed to display with error: " + arg2.Message);
        }
        
        private void OnInterstitialClickedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
            
            onInterstitialClicked.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Interstitial clicked");
        }

        private void OnInterstitialHiddenEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
            
            onInterstitialHidden.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Interstitial hidden");
        }

        private void OnInterstitialDisplayedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            // Interstitial ad is hidden. Pre-load the next ad
            MaxSdk.LoadInterstitial(interstitialAdUnitId);
            
            onInterstitialDisplayed.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("MaxSDK: Interstitial displayed");
            
        }

        #endregion

        #region Rewarded

        private void InitializeRewardedAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdDismissedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;

            // Load the first RewardedAd
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
            
            if (reportInDebugLog)
                Debug.Log("Initialize Rewarded Ads");
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

            // Reset retry attempt
            _rewardedRetryAttempt = 0;
            
            onRewardedAdLoaded.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad loaded");
        }


        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            _rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, _rewardedRetryAttempt));

            void LoadRewardedAd() => MaxSdk.LoadRewardedAd(rewardedAdUnitId);
            
            Invoke(nameof(LoadRewardedAd), (float) retryDelay);
            
            onRewardedAdFailed.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad failed to load with error code: " + errorInfo);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad failed to display. We recommend loading the next ad
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
            
            onRewardedAdFailedToDisplay.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad failed to display with error code: " + errorInfo);
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            onRewardedAdDisplayed.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad displayed");
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            onRewardedAdClicked.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad clicked");
        }

        private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            onRewardedAdDismissed.Invoke();
            
            MaxSdk.LoadRewardedAd(rewardedAdUnitId);
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad dismissed");
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad was displayed and user should receive the reward
            onRewardedAdReceivedReward.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad received reward");
        }

        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad was displayed and user should receive the reward
            onRewardedAdHidden.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Rewarded ad hidden reward");
        }

        #endregion
    
        #region Rewarded Interstitial Ad Methods

        private void InitializeRewardedInterstitialAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnRewardedInterstitialAdLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnRewardedInterstitialAdFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnRewardedInterstitialAdDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnRewardedInterstitialAdClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnRewardedInterstitialAdFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnRewardedInterstitialAdDismissedEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnRewardedInterstitialAdReceivedRewardEvent;

            // Load the first RewardedInterstitialAd
            LoadRewardedInterstitialAd();
        }

        private void LoadRewardedInterstitialAd()
        {
            MaxSdk.LoadRewardedInterstitialAd(rewardedInterstitialAdUnitId);
        }

        private void ShowRewardedInterstitialAd()
        {
            if (MaxSdk.IsRewardedInterstitialAdReady(rewardedInterstitialAdUnitId))
                MaxSdk.ShowRewardedInterstitialAd(rewardedInterstitialAdUnitId);
        }

        private void OnRewardedInterstitialAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad is ready to be shown. MaxSdk.IsRewardedInterstitialAdReady(rewardedInterstitialAdUnitId) will now return 'true'
            Debug.Log("Rewarded interstitial ad loaded");
        
            // Reset retry attempt
            _rewardedInterstitialRetryAttempt = 0;
        }

        private void OnRewardedInterstitialAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Rewarded interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            _rewardedInterstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, _rewardedInterstitialRetryAttempt));
        
            Debug.Log("Rewarded interstitial ad failed to load with error: " + errorInfo);
        
            Invoke("LoadRewardedInterstitialAd", (float) retryDelay);
        }

        private void OnRewardedInterstitialAdFailedToDisplayEvent(string adUnitId,  MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad failed to display. We recommend loading the next ad
            Debug.Log("Rewarded interstitial ad failed to display with error: " + errorInfo);
            LoadRewardedInterstitialAd();
        }

        private void OnRewardedInterstitialAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded interstitial ad displayed");
        }

        private void OnRewardedInterstitialAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded interstitial ad clicked");
        }

        private void OnRewardedInterstitialAdDismissedEvent(string adUnitId,  MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad is hidden. Pre-load the next ad
            Debug.Log("Rewarded interstitial ad dismissed");
            LoadRewardedInterstitialAd();
        }

        private void OnRewardedInterstitialAdReceivedRewardEvent(string adUnitId,  MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded interstitial ad was displayed and user should receive the reward
            Debug.Log("Rewarded interstitial ad received reward");
        }

        #endregion

        #region Banner

        private void InitializeBannerAds()
        {
            // Banners are automatically sized to 320x50 on phones and 728x90 on tablets.
            // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments.
            MaxSdk.CreateBanner(bannerAdUnitId, bannerPosition);

            // Set background or background color for banners to be fully functional.
            MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerLoaded;
            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerClicked;
            
            if (reportInDebugLog)
                Debug.Log("Initialize Banner Ads");
        }

        private void OnBannerLoaded(string arg1, MaxSdkBase.AdInfo arg2)
        {
            onBannerLoaded.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Banner Loaded");
        }
        
        private void OnBannerClicked(string arg1, MaxSdkBase.AdInfo arg2)
        {
            onBannerClicked.Invoke();
            
            if (reportInDebugLog)
                Debug.Log("Banner Clicked");
        }

        #endregion

        #region MREC Ad Methods

        private void InitializeMRecAds()
        {
            // MRECs are automatically sized to 300x250.
            MaxSdk.CreateMRec(mRecAdUnitId, MaxSdkBase.AdViewPosition.BottomCenter);
        }

        private void ToggleMRecVisibility()
        {
            if (!_isMRecShowing)
            {
                MaxSdk.ShowMRec(mRecAdUnitId);
            }
            else
            {
                MaxSdk.HideMRec(mRecAdUnitId);
            }

            _isMRecShowing = !_isMRecShowing;
        }

        #endregion
    }
}