using System;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Components;
using Krem.AzureAnalytics.Types;

namespace Krem.AzureAnalytics.Actions.AppMetricaActions
{
    [NodeGraphGroupName("Azure Analytics/AppMetrica")]
    public class ReportSkinUnlock : CoreAction
    {
        [InjectComponent] private AppMetricaReporter _appMetricaReporter;
        
        [ActionParameter] public SkinType skinType;
        [ActionParameter] public String skinName;
        [ActionParameter] public SkinRarity skinRarity;
        [ActionParameter] public SkinUnlockType skinUnlockType;

        protected override bool Action()
        {
            _appMetricaReporter.eventParameters.Clear();
            
            FillEventParameters();
            _appMetricaReporter.ReportEvent(EventNames.skin_unlock.ToString());

            return true;
        }

        protected void FillEventParameters()
        {
            _appMetricaReporter.eventParameters[SkinParams.skin_type.ToString()] = skinType.ToString();
            _appMetricaReporter.eventParameters[SkinParams.skin_name.ToString()] = skinName;
            _appMetricaReporter.eventParameters[SkinParams.skin_rarity.ToString()] = skinRarity.ToString();
            _appMetricaReporter.eventParameters[SkinParams.unlock_type.ToString()] = skinUnlockType.ToString();
        }
    }
}