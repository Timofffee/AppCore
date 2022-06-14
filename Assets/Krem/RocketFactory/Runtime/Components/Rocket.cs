using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using UnityEngine;

namespace Krem.RocketFactory.Components
{
    [NodeGraphGroupName("Rocket Factory")]
    [DisallowMultipleComponent]
    public class Rocket : CoreComponent
    {
        [Header("Settings")]
        [SerializeField, NotNull] protected GameObject _segmentContainer;
        [SerializeField] protected bool _active = true;

        [Header("Data")]
        [SerializeField] protected List<RocketSegment> _segments = new List<RocketSegment>();

        [Header("Ports")]
        [BindInputSignal(nameof(ClearRequest))] public InputSignal CallClearRequest;
        [BindInputSignal(nameof(RefreshRequest))] public InputSignal CallRefreshRequest;
        public OutputSignal OnClearRequest;
        public OutputSignal OnRefreshRequest;
        
        public GameObject SegmentContainer => _segmentContainer;
        public bool Active
        {
            get => _active;
            set => _active = value;
        }
        public List<RocketSegment> Segments => _segments;

        public void ClearRequest()
        {
            OnClearRequest.Invoke();
        }

        public void RefreshRequest()
        {
            OnRefreshRequest.Invoke();
        }
    }
}