using System.Collections.Generic;
using Krem.AppCore.Extensions;
using Krem.RocketFactory.Components;
using Krem.RocketFactory.Models;
using UnityEngine;

namespace Krem.RocketFactory.Services
{
    public static class RocketBuilderService
    {
        public static void AutogenerateSegmentTypes(List<RocketSegmentModel> segments, RocketSegmentScriptableCollection dataSource)
        {
            int totalSegmentCount = segments.Count;
            RocketSegmentType currentSegmentType = RocketSegmentType.Tail;
            if (totalSegmentCount <= 4)
                currentSegmentType = RocketSegmentType.Thin;

            for (int i = 0; i < totalSegmentCount; i++)
            {
                RocketSegmentModel currentSegmentPrefab = segments[i];
                currentSegmentPrefab.SegmentType = currentSegmentType;

                switch (currentSegmentType)
                {
                    case RocketSegmentType.Tail:
                        SyncRocketSegmentWithDataSet(currentSegmentPrefab, dataSource);
                        currentSegmentType = currentSegmentType.Prev();
                        break;

                    case RocketSegmentType.Wide:
                        SyncRocketSegmentWithDataSet(currentSegmentPrefab, dataSource);
                        if (i >= Mathf.RoundToInt(totalSegmentCount / 2f) - 2)
                            currentSegmentType = currentSegmentType.Prev();
                        break;

                    case RocketSegmentType.TransitionToWide:
                        SyncRocketSegmentWithDataSet(currentSegmentPrefab, dataSource);
                        currentSegmentType = currentSegmentType.Prev();
                        break;

                    case RocketSegmentType.Thin:
                        SyncRocketSegmentWithDataSet(currentSegmentPrefab, dataSource);
                        if (i >= totalSegmentCount - 2)
                            currentSegmentType = currentSegmentType.Prev();
                        break;

                    case RocketSegmentType.Head:
                        SyncRocketSegmentWithDataSet(currentSegmentPrefab, dataSource);
                        break;
                }
            }
        }

        public static void SyncRocketSegmentWithDataSet(RocketSegmentModel rocketSegmentModel, RocketSegmentScriptableCollection dataSource)
        {
            RocketSegmentModel randomSegmentOfType = dataSource.GetRandomOfType(rocketSegmentModel.SegmentType);
            
            rocketSegmentModel.Name = randomSegmentOfType.Name;
            rocketSegmentModel.SegmentPrefab = randomSegmentOfType.SegmentPrefab;
            rocketSegmentModel.Height = randomSegmentOfType.Height;
        }
    }
}
