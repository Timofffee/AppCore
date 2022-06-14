using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.RocketFactory.Components;
using UnityEngine;

namespace Krem.RocketFactory.Actions
{
    [NodeGraphGroupName("Rocket Factory")] 
    public class ClearRocket : CoreAction
    {
        [InjectComponent] private Rocket _rocket;
        
        protected override bool Action()
        {
            _rocket.Segments.ForEach(segment =>
            {
                GameObject.Destroy(segment.gameObject);
            });

            _rocket.Segments.Clear();
        
            return true;
        }
    }
}