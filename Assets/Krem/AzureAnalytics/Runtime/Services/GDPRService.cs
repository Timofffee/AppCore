using UnityEngine;

namespace Krem.AzureAnalytics.Services
{
    public static class GDPRService
    {
        private const string GDPR_CURRENT_STATE = "gdpr_current_state";

        public static bool GetAppliedState()
        {
            return PlayerPrefs.GetInt(GDPR_CURRENT_STATE) == 1;
        }

        public static void SetAppliedState(bool state)
        {
            PlayerPrefs.SetInt(GDPR_CURRENT_STATE, state ? 1 : 0);
        }
    }
}