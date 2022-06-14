using System.Collections.Generic;
using System.Linq;
using Krem.AppCore.Extensions;
using Krem.JetPack.ScriptableORM;
using UnityEngine;

namespace Krem.RocketFactory.Models
{
    [CreateAssetMenu(fileName = "RocketSegmentScriptableCollection", menuName = "Rocket Factory/RocketSegmentScriptableCollection", order = 0)]
    public class RocketSegmentScriptableCollection : ScriptableCollection<RocketSegmentModel>
    {
        public RocketSegmentModel GetRandomOfType(RocketSegmentType segmentType)
        {
            IEnumerable<RocketSegmentModel> filteringQuery =
                from segment in Collection
                where segment.SegmentType == segmentType
                select segment;

            List<RocketSegmentModel> result = filteringQuery.ToList();
            
            return result.Random();
        }
    }
}
