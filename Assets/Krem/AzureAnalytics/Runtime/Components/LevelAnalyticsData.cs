using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Services;
using Krem.AzureAnalytics.Types;
using UnityEngine;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class LevelAnalyticsData : CoreComponent 
    {    
        [Header("Data")]
        public int levelNumber = 0;
        public string levelName;
        public LevelDifficult levelDifficult = LevelDifficult.normal;
        public LevelType levelType = LevelType.normal;
        public int levelLoop = 1;
        public bool levelRandom = false;
        public LevelResult levelResult;
        public int levelProgress = 0;
        public int continues = 0;

        public int LevelCounterIndex => LevelCounterService.GetLevelIndex();
    }
}