using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Components;
using Krem.AzureAnalytics.Types;
using UnityEngine;

namespace Krem.AzureAnalytics.Actions.AppMetricaActions
{
    [NodeGraphGroupName("Azure Analytics/AppMetrica")]
    public class ReportTutorial : CoreAction
    {
        [InjectComponent] private AppMetricaReporter _appMetricaReporter;
        
        [Header("Data")]
        public string stepName = "01_StepName";

        protected override bool Action()
        {
            _appMetricaReporter.eventParameters.Clear();
            
            FillEventParameters();
            _appMetricaReporter.ReportEvent(EventNames.tutorial.ToString());

            return true;
        }

        protected void FillEventParameters()
        {
            _appMetricaReporter.eventParameters[TutorialParams.step_name.ToString()] = stepName;
        }
    }
}