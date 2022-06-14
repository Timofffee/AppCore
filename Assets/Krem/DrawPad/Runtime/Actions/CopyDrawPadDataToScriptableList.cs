using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DrawPad.Components;
using Krem.DrawPad.Models;

namespace Krem.DrawPad.Actions
{
    [NodeGraphGroupName("DrawPad")]
    public class CopyDrawPadDataToScriptableList : CoreAction
    {
        public InputComponent<SegmentCollectionProvider> segmentModelListProvider;
        
        [InjectComponent] private Components.DrawPad _drawPad;

        protected override bool Action()
        {
            segmentModelListProvider.Component.Collection.Clear();
            
            _drawPad.DrawnSegments.ForEach(segment =>
            {
                SegmentModel segmentModel = new SegmentModel();
                segmentModel.position = segment.RectTransform.position;
                segmentModel.rotation = segment.RectTransform.rotation;
                segmentModel.sizeDelta = segment.RectTransform.sizeDelta;
                segmentModel.anchoredPosition = segment.RectTransform.anchoredPosition;
                    
                segmentModelListProvider.Component.Collection.Add(segmentModel);
            });

            return true;
        }
    }
}