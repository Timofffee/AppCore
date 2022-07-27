using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Services;
using UnityEngine;

namespace Krem.AzureAnalytics.Actions.MaxSdkActions
{
    [NodeGraphGroupName("Azure Analytics/MaxSDK")]
    public class CanShowInterstitialCondition : CoreAction
    {
        [Header("Show Settings")]
        [ActionParameter] public int showEveryLevels = 3;
        [ActionParameter] public float showEverySeconds = 180;

        [Header("Ports")]
        public OutputSignal OnInterstitialCanShown;
        public OutputSignal OnInterstitialCantShown;

        public static float timeOffset = 0;
        public static int levelOffset = 0;
        
        protected override bool Action()
        {
            float currentTime = Time.time - timeOffset;
            int currentLevel = LevelCounterService.GetLevelIndex() - levelOffset;

            if (currentLevel > 0 && (currentLevel % showEveryLevels == 0 || currentTime >= showEverySeconds))
            {
                Reset();
                
                OnInterstitialCanShown.Invoke();
            }
            else
            {
                OnInterstitialCantShown.Invoke();
            }

            return true;
        }

        public void Reset()
        {
            timeOffset = Time.time;
            levelOffset = LevelCounterService.GetLevelIndex();
        }
        
    }
}