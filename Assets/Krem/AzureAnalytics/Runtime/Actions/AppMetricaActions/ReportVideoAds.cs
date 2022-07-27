using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Components;
using Krem.AzureAnalytics.Types;

namespace Krem.AzureAnalytics.Actions.AppMetricaActions
{
    [NodeGraphGroupName("Azure Analytics/AppMetrica")]
    public class ReportVideoAds : CoreAction
    {
        [InjectComponent] private AppMetricaReporter _appMetricaReporter;
        [InjectComponent] private VideoAdsData _adsData;

        [ActionParameter] public AdResult adResult;
        [ActionParameter] public EventNames eventName;
        [ActionParameter] public bool reportEventToAppMetrica = true;

        public InputSignal Enable;
        public InputSignal Disable;

        protected override bool Action()
        {
            if (reportEventToAppMetrica == false)
            {
                return false;
            }
            
            _appMetricaReporter.eventParameters.Clear();
            FillEventParameters();
            _appMetricaReporter.ReportEvent(eventName.ToString());

            return true;
        }

        protected void FillEventParameters()
        {
            _appMetricaReporter.eventParameters[AdParams.ad_type.ToString()] = _adsData.adType.ToString();
            _appMetricaReporter.eventParameters[AdParams.placement.ToString()] = _adsData.adPlacement.ToString();
            _appMetricaReporter.eventParameters[AdParams.result.ToString()] = adResult.ToString();
            _appMetricaReporter.eventParameters[AdParams.connection.ToString()] = _appMetricaReporter.InternetStatus;
        }
    }
}