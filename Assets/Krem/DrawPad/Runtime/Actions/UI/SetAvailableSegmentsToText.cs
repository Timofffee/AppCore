using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.DrawPad.Components;

namespace Krem.DrawPad.Actions.UI
{
    [NodeGraphGroupName("DrawPad/UI")]
    public class SetAvailableSegmentsToText : CoreAction
    {
        [InjectComponent] private SegmentsCounter _segmentsCounter;
        
        protected override bool Action()
        {
            _segmentsCounter.CountText.text = _segmentsCounter.DrawPad.AvailableSegmentsCount.ToString();

            return true;
        }
    }
}
