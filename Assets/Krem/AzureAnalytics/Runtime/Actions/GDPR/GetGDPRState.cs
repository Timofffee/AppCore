using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.AzureAnalytics.Services;

namespace Krem.AzureAnalytics.Actions.GDPR
{
    [NodeGraphGroupName("Azure Analytics/GDPR")]
    public class GetGDPRState : CoreAction 
    {
        public OutputSignal OnApplies;
        public OutputSignal OnDoesNotApplies;
    
        protected override bool Action()
        {
            if (GDPRService.GetAppliedState())
                OnApplies.Invoke();
            else
                OnDoesNotApplies.Invoke();
        
            return true;
        }
    }
}