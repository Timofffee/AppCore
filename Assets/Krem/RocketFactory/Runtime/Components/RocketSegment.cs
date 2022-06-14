using Krem.AppCore.Attributes;
using Krem.JetPack.Basis.Components.Links;
using Krem.RocketFactory.Models;
using UnityEngine;

namespace Krem.RocketFactory.Components
{
    [NodeGraphGroupName("Rocket Factory")]
    public class RocketSegment : TransformLink
    {
        [SerializeField] protected RocketSegmentModel _rocketSegmentModel;
        
        public RocketSegmentModel RocketSegmentModel { get => _rocketSegmentModel; set => _rocketSegmentModel = value; }
        public GameObject ViewInstance { get; set; }
    }
}