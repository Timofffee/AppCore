using System.Collections.Generic;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.AzureAnalytics.Components
{
    [NodeGraphGroupName("Azure Analytics")]
    [DisallowMultipleComponent]
    public class AppMetricaReporter : CoreComponent
    {
        [Header("Settings")]
        public bool debugLog = false;

        [Header("Ports")]
        [BindInputSignal(nameof(RequestReport))] public InputSignal CallRequestReport;
        public OutputSignal OnReportRequested;
        public OutputSignal OnReported;
        
        public Dictionary<string, object> eventParameters = new Dictionary<string, object>();
        
        private static float _resetTime;

        public int ElapsedTime => (int) (Time.unscaledTime - _resetTime);
        public bool InternetStatus => Application.internetReachability != NetworkReachability.NotReachable;

        public void ResetTimer()
        {
            _resetTime = Time.unscaledTime;
        }

        public void ReportEvent(string eventName)
        {
            if (debugLog)
                Debug.Log("AzureAnalytics Report Event: " + eventName.ToString());
            
            AppMetrica.Instance.ReportEvent(eventName, eventParameters);
            AppMetrica.Instance.SendEventsBuffer();
            
            OnReported.Invoke();
        }

        public void RequestReport()
        {
            OnReportRequested.Invoke();
        }
    }
}