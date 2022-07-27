using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Components;
using Krem.AzureAnalytics.Services;
using Krem.AzureAnalytics.Types;

namespace Krem.AzureAnalytics.Actions.AppMetricaActions
{
    [NodeGraphGroupName("Azure Analytics/AppMetrica")]
    public class ReportLevelStart : CoreAction
    {
        [InjectComponent] private LevelAnalyticsData _levelAnalyticsData;
        [InjectComponent] private AppMetricaReporter _appMetricaReporter;

        protected override bool Action()
        {
            LevelCounterService.IncreaseLevelIndex();
            _appMetricaReporter.ResetTimer();
            _appMetricaReporter.eventParameters.Clear();
            
            FillEventParameters();
            _appMetricaReporter.ReportEvent(EventNames.level_start.ToString());

            return true;
        }

        protected void FillEventParameters()
        {
            if (_levelAnalyticsData.levelNumber > 0)
                _appMetricaReporter.eventParameters[LevelParams.level_number.ToString()] = _levelAnalyticsData.levelNumber;
            if (!String.IsNullOrEmpty(_levelAnalyticsData.levelName))
                _appMetricaReporter.eventParameters[LevelParams.level_name.ToString()] = _levelAnalyticsData.levelName.Replace(" ", "_").ToLower();
            _appMetricaReporter.eventParameters[LevelParams.level_count.ToString()] = _levelAnalyticsData.LevelCounterIndex;
            
            _appMetricaReporter.eventParameters[LevelParams.level_diff.ToString()] = _levelAnalyticsData.levelDifficult.ToString();
            _appMetricaReporter.eventParameters[LevelParams.level_type.ToString()] = _levelAnalyticsData.levelType.ToString();
            _appMetricaReporter.eventParameters[LevelParams.level_loop.ToString()] = _levelAnalyticsData.levelLoop;
            _appMetricaReporter.eventParameters[LevelParams.level_random.ToString()] = _levelAnalyticsData.levelRandom;
        }
    }
}