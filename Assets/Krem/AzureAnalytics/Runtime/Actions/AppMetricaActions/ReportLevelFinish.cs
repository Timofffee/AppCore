using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Components;
using Krem.AzureAnalytics.Services;
using Krem.AzureAnalytics.Types;

namespace Krem.AzureAnalytics.Actions.AppMetricaActions
{
    [NodeGraphGroupName("Azure Analytics/AppMetrica")]
    public class ReportLevelFinish : CoreAction
    {
        [InjectComponent] private LevelAnalyticsData _levelAnalyticsData;
        [InjectComponent] private AppMetricaReporter _appMetricaReporter;

        protected override bool Action()
        {
            _appMetricaReporter.eventParameters.Clear();

            FillEventParameters();
            _appMetricaReporter.ReportEvent(EventNames.level_finish.ToString());

            return true;
        }

        protected void FillEventParameters()
        {
            if (_levelAnalyticsData.levelNumber > 0)
                _appMetricaReporter.eventParameters[LevelParams.level_number.ToString()] = _levelAnalyticsData.levelNumber;
            if (!String.IsNullOrEmpty(_levelAnalyticsData.levelName))
                _appMetricaReporter.eventParameters[LevelParams.level_name.ToString()] = _levelAnalyticsData.levelName.Replace(" ", "_").ToLower();
            _appMetricaReporter.eventParameters[LevelParams.level_count.ToString()] = LevelCounterService.GetLevelIndex();
            
            _appMetricaReporter.eventParameters[LevelParams.level_diff.ToString()] = _levelAnalyticsData.levelDifficult.ToString();
            _appMetricaReporter.eventParameters[LevelParams.level_type.ToString()] = _levelAnalyticsData.levelType.ToString();
            _appMetricaReporter.eventParameters[LevelParams.level_loop.ToString()] = _levelAnalyticsData.levelLoop;
            _appMetricaReporter.eventParameters[LevelParams.level_random.ToString()] = _levelAnalyticsData.levelRandom;
            
            // Level result parameters
            _appMetricaReporter.eventParameters[LevelParams.result.ToString()] = _levelAnalyticsData.levelResult.ToString();
            _appMetricaReporter.eventParameters[LevelParams.time.ToString()] = _appMetricaReporter.ElapsedTime;
            _appMetricaReporter.eventParameters[LevelParams.progress.ToString()] = _levelAnalyticsData.levelProgress;
            _appMetricaReporter.eventParameters[LevelParams.continues.ToString()] = _levelAnalyticsData.continues;
        }
    }
}