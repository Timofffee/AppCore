using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AzureAnalytics.Services;

namespace Krem.AzureAnalytics.Actions.GDPR
{
    [NodeGraphGroupName("Azure Analytics/GDPR")]
    public class SetUserConsent : CoreAction
    {
        [ActionParameter] public bool state;
    
        protected override bool Action()
        {
            GDPRService.SetAppliedState(state);
            MaxSdk.SetHasUserConsent(state);

            return true;
        }
    }
}