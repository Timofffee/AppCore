using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.RocketFactory.Components;
using UnityEngine;

namespace Krem.RocketFactory.Actions
{
    [NodeGraphGroupName("Rocket Factory")] 
    public class BuildRocketFromCollection : CoreAction
    {
        public InputComponent<RocketSegmentCollectionProvider> RocketSegmentCollectionProvider;

        [InjectComponent] private Rocket _rocket;
        [InjectComponent] private Transform _transform;
        
        protected override bool Action()
        {
            if (_rocket.Segments.Count > 0)
            {
                Debug.LogWarning("Can`t Build Rocket, Current Rocket nor cleared.");

                return false;
            }

            if (RocketSegmentCollectionProvider.Component.Collection.Count == 0)
            {
                Debug.LogWarning("Can`t Build Rocket, Input SegmentCollection are empty");

                return false;
            }
            
            Vector3 currentSegmentPosition = Vector3.zero;
            RocketSegmentCollectionProvider.Component.Collection.ForEach(segmentModel =>
            {
                GameObject rocketSegmentInstance = GameObject.Instantiate(
                    _rocket.SegmentContainer,
                    _transform
                );

                if (rocketSegmentInstance.TryGetComponent<RocketSegment>(out RocketSegment segment))
                {
                    segment.RocketSegmentModel = segmentModel;
                }

                segment.ViewInstance = GameObject.Instantiate(
                    segmentModel.SegmentPrefab,
                    segment.Transform
                );

                segment.Transform.localPosition = currentSegmentPosition;
                segment.Transform.localRotation = segment.RocketSegmentModel.Rotation;
                    
                Vector3 offset = (segment.RocketSegmentModel.Rotation * Vector3.up) * segment.RocketSegmentModel.Height;
                currentSegmentPosition += offset;
                
                _rocket.Segments.Add(segment);
            });
        
            return true;
        }
    }
}