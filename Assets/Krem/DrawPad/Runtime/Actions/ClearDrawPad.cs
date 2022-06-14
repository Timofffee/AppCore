using Krem.AppCore;
using Krem.AppCore.Attributes;

namespace Krem.DrawPad.Actions
{
    [NodeGraphGroupName("DrawPad")]
    public class ClearDrawPad : CoreAction
    {
        [InjectComponent] private Components.DrawPad _drawPad;
        
        protected override bool Action()
        {
            
            _drawPad.DrawnSegments.ForEach(item =>
            {
                item.ReturnToPool();
            });
            _drawPad.DrawnSegments.Clear();
            _drawPad.CurrentShadowSegment.gameObject.SetActive(false);

            return true;
        }
    }
}