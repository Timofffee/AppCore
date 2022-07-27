using UnityEngine;

namespace Krem.AzureAnalytics.Services
{
    public static class LevelCounterService
    {
        private const string LEVEL_COUNTER = "analytics_level_counter";

        public static int GetLevelIndex()
        {
            int currentLevelCount = PlayerPrefs.GetInt(LEVEL_COUNTER);

            return currentLevelCount;
        }

        public static void SetLevelIndex(int currentLevelCount)
        {
            PlayerPrefs.SetInt(LEVEL_COUNTER, currentLevelCount);
        }

        public static int IncreaseLevelIndex()
        {
            int currentLevelCount = GetLevelIndex();
            currentLevelCount++;
            SetLevelIndex(currentLevelCount);

            return currentLevelCount;
        }
    }
}