using Krem.AppCore;
using Krem.AppCore.Attributes;
using Krem.AppCore.Ports;
using Krem.DrawPad.Components;
using UnityEngine;

namespace Krem.DrawPad.Actions
{
    [NodeGraphGroupName("DrawPad")]
    public class RestoreDrawPadFromScriptable : CoreAction
    {
        [InjectComponent] private Components.DrawPad _drawPad;
        
        public InputComponent<SegmentCollectionProvider> segmentModelListProvider;
        
        protected override bool Action()
        {
            _drawPad.DrawnSegments.Clear();
            
            if (segmentModelListProvider.Component.Collection.Count == 0)
                return true;

            segmentModelListProvider.Component.Collection.ForEach(item =>
            {
                DrawPadSegment segment = _drawPad.GetFromPool();
                segment.RectTransform.sizeDelta = item.sizeDelta;
                segment.RectTransform.anchoredPosition = item.anchoredPosition;
                segment.RectTransform.rotation = item.rotation;
                    
                _drawPad.DrawnSegments.Add(segment);
            });

            return true;
        }
    }
}