using Krem.AppCore;
using Krem.AppCore.Attributes;
using UnityEngine;

namespace Krem.AzureAnalytics.Actions.GDPR
{
    [NodeGraphGroupName("Azure Analytics/GDPR")]
    public class OpenURL : CoreAction
    { 
        [ActionParameter] public string url = "https://privacy.azurgames.com/";
        
        protected override bool Action()
        {
            Application.OpenURL(url);
            
            return true;
        }
    }
}
