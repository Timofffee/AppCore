using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Types;
using UnityEngine;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class BannerAdsData : CoreComponent 
    {    
        [Header("Data")]
        public AdType adType;
        public BannerPlacement bannerPlacement; 
        public AdResult adResult;
    }
}